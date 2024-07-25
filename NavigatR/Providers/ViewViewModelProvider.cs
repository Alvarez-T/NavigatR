using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using NavigatR.Services;

namespace NavigatR.Providers;

internal sealed class ViewViewModelProvider : IViewProvider, IViewModelProvider
{
    private readonly IServiceProvider _serviceProvider;

    private readonly MapViewAndViewModel _map;

    public ViewViewModelProvider(ICollection<ServiceDescriptor> viewModels, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _map = new MapViewAndViewModel(viewModels);
    }

    public TView GetViewFromViewModel<TView>(IViewModel viewModel) where TView : class
    {
        Type viewType = _map.TryGetViewTypeByViewModel(viewModel.GetType());

        if (!viewType.IsAssignableTo(typeof(TView)))
            throw new InvalidCastException($"The view is not a type of {typeof(TView).Name}.");

        return (TView)_serviceProvider.GetRequiredService(viewType);
    }

    public TViewModel GetViewModelFromView<TViewModel>(Type view) where TViewModel : IViewModel
    {
        Type viewModelType = _map.TryGetViewModelTypeByView(view);

        if (!viewModelType.IsAssignableTo(typeof(TViewModel)))
            throw new InvalidCastException($"The view model is not a type of {typeof(TViewModel).Name}.");

        return (TViewModel)_serviceProvider.GetRequiredService(viewModelType);
    }

    public TView GetView<TView>() where TView : class
        => _serviceProvider.GetRequiredService<TView>();

    public TViewModel GetViewModel<TViewModel>() where TViewModel : IViewModel
        => _serviceProvider.GetRequiredService<TViewModel>();
}