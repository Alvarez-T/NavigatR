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

    public TViewModel GetViewModel<TViewModel>() where TViewModel : IViewModel
        => _serviceProvider.GetRequiredService<TViewModel>();

    public TViewModel GetViewModelFromView<TViewModel>(Type view) where TViewModel : IViewModel
    {
        Type viewModelType = _map.GetViewModelType(view);

        if (!viewModelType.IsAssignableTo(typeof(TViewModel)))
            throw new InvalidCastException($"The view model is not a type of {typeof(TViewModel).Name}.");

        return (TViewModel)_serviceProvider.GetRequiredService(viewModelType);
    }

    public IViewModel GetViewModelFromView(Type view)
    {
        Type viewModelType = _map.GetViewModelType(view);

        return (IViewModel)_serviceProvider.GetRequiredService(viewModelType);
    }

    public TView GetView<TView>() where TView : class
        => _serviceProvider.GetRequiredService<TView>();

    public TView GetViewFromViewModel<TView>(IViewModel viewModel) where TView : class
    {
        Type viewType = _map.GetViewType(viewModel.GetType());

        if (!viewType.IsAssignableTo(typeof(TView)))
            throw new InvalidCastException($"The view is not a type of {typeof(TView).Name}.");

        return (TView)_serviceProvider.GetRequiredService(viewType);
    }

    public object GetViewFromViewModel<TViewModel>() where TViewModel : class, IViewModel
    {
        TViewModel viewModel = GetViewModel<TViewModel>();
        return GetViewFromViewModel<object>(viewModel);
    }
}