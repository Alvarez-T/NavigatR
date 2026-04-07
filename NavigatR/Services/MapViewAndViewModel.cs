using Microsoft.Extensions.DependencyInjection;
using NavigatR.Exceptions;
using System.Collections.Frozen;

namespace NavigatR.Services
{
    public class MapViewAndViewModel
    {
        private readonly FrozenDictionary<Type, Type> _viewToViewModel;
        private readonly FrozenDictionary<Type, Type> _viewModelToView;

        public MapViewAndViewModel(IEnumerable<ServiceDescriptor> descriptors)
        {
            // Use standard Dictionary for the building phase (O(1) lookup/insert)
            var forward = new Dictionary<Type, Type>();
            var reverse = new Dictionary<Type, Type>();

            foreach (var descriptor in descriptors)
            {
                var viewType = descriptor.ServiceType;
                var viewModelType = descriptor.ImplementationType;

                // 1. Safety Check: Skip if registered via Factory (e.g., services.AddSingleton(p => new VM()))
                // because ImplementationType will be null.
                if (viewModelType == null) continue;

                // 2. Check for "One View -> Many ViewModels" violation
                if (forward.TryGetValue(viewType, out var existingViewModel))
                {
                    // If the same pair is registered twice, we might want to ignore it. 
                    // But if the View maps to a DIFFERENT ViewModel, throw.
                    if (existingViewModel != viewModelType)
                    {
                        throw new OneViewToManyViewModelNotAllowedException(viewType, existingViewModel, viewModelType);
                    }

                    // If it's the exact same pair, just skip to avoid "Item already added" error
                    continue;
                }

                // 3. Check for Reverse violation (One ViewModel -> Many Views)
                // If you want strict 1:1 bi-directional mapping, you must check this too.
                if (reverse.ContainsKey(viewModelType))
                {
                    throw new InvalidOperationException($"The ViewModel '{viewModelType.Name}' is already mapped to a View. Strict 1:1 mapping is required.");
                }

                // Add to builders
                forward.Add(viewType, viewModelType);
                reverse.Add(viewModelType, viewType);
            }

            // Convert to Frozen for maximum read performance
            _viewToViewModel = forward.ToFrozenDictionary();
            _viewModelToView = reverse.ToFrozenDictionary();
        }

        // RENAMED: 'Get' implies it throws if missing
        public Type GetViewModelType(Type view)
        {
            if (!_viewToViewModel.TryGetValue(view, out var viewModel))
            {
                throw new InvalidOperationException($"No ViewModel registered for View '{view.Name}'.");
            }
            return viewModel;
        }

        public Type GetViewType(Type viewModel)
        {
            // FIX: viewModel.GetType().Name would return "RuntimeType". Use viewModel.Name.
            if (!_viewModelToView.TryGetValue(viewModel, out var view))
            {
                throw new InvalidOperationException($"No View registered for ViewModel '{viewModel.Name}'.");
            }
            return view;
        }

        // ADDED: True 'Try' pattern support (optional but recommended)
        public bool TryGetViewModelType(Type view, out Type? viewModel)
        {
            return _viewToViewModel.TryGetValue(view, out viewModel);
        }
    }
}