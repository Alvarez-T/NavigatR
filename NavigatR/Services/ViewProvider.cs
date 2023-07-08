using Microsoft.Extensions.DependencyInjection;

namespace NavigatR.Services;

public sealed class ViewProvider
{
    private readonly IViewFactory _viewFactory;

    private Dictionary<Type, Type> _viewModelDictionary;

    public ViewProvider(ICollection<ServiceDescriptor> viewModels, IViewFactory viewFactory)
    {
        _viewFactory = viewFactory;
        _viewModelDictionary = viewModels.ToDictionary(viewModel => viewModel.ImplementationType!, view => view.ServiceType);
    }

    public TView GetViewFromViewModel<TView>(IViewModel viewModel) where TView : class
    {
        Type viewType;
        if (!_viewModelDictionary.TryGetValue(viewModel.GetType(), out viewType!))
            throw new InvalidOperationException($"It was not found a View for the {viewModel.GetType().Name}. The View and View Model must be registered.");

        if (!viewType.IsAssignableTo(typeof(TView)))
            throw new InvalidCastException($"The view is not a type of {typeof(TView).Name}.");

        return (TView)_viewFactory.CreateView(viewType);
    }
}
