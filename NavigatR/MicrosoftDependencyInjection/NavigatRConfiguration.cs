using Microsoft.Extensions.DependencyInjection.Extensions;
using NavigatR;
using NavigatR.Providers;

namespace Microsoft.Extensions.DependencyInjection;

public class NavigatRConfiguration
{
    private readonly IServiceCollection _services;

    private readonly List<ServiceDescriptor> _viewModelDescriptors = [];

    private bool _useViewLocator = true;
    private bool _useViewModelLocator = true;

    public NavigatRConfiguration(IServiceCollection services)
    {
        _services = services;
    }

    internal NavigatRConfiguration ConfigureNavigatRLibrary()
    {
        foreach (var viewModelDescriptor in _viewModelDescriptors)
        {
            _services.TryAddTransient(viewModelDescriptor.ServiceType);
            _services.TryAddTransient(viewModelDescriptor.ImplementationType!);
        }

        _services.AddTransient<INavigator, Navigator>();

        ConfigureProviders();

        return this;
    }

    private void ConfigureProviders()
    {
        _services.AddSingleton((serviceProvider) => new ViewViewModelProvider(_viewModelDescriptors, serviceProvider));
        _services.AddSingleton<IViewProvider>(provider =>
        {
            IViewProvider viewProvider = provider.GetRequiredService<ViewViewModelProvider>();
            if (_useViewLocator)
                ViewLocator.CreateViewLocator(viewProvider);
            return viewProvider;
        });
        _services.AddSingleton<IViewModelProvider>(provider =>
        {
            IViewModelProvider viewModelProvider = provider.GetRequiredService<ViewViewModelProvider>();
            if (_useViewModelLocator)
                ViewModelLocator.CreateViewModelLocator(viewModelProvider);
            return viewModelProvider;
        });
        _services.AddSingleton<INavigatorProvider, NavigatorProvider>(provider =>
        {
            var navigatorProvider = provider.GetRequiredService<INavigatorProvider>();
            NavigatorLocator.CreateNavigatorLocator(navigatorProvider);
            return (NavigatorProvider)navigatorProvider;
        });
    }

    public NavigatRConfiguration UseViewLocator(bool useIt = true)
    {
        _useViewLocator = useIt;
        return this;
    }

    public NavigatRConfiguration UseViewModelLocator(bool useIt = true)
    {
        _useViewModelLocator = useIt;
        return this;
    }

    public NavigatRViewConfiguration RegisterViewModel<TViewModel, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TViewModel : class, IViewModel
    {
        _services.Add(ServiceDescriptor.Describe(typeof(TViewModel), typeof(TImplementation), lifetime));
        return new NavigatRViewConfiguration(this, _services, typeof(TImplementation));
    }

    public NavigatRViewConfiguration RegisterViewModel<TViewModel>(ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        _services.Add(ServiceDescriptor.Describe(typeof(TViewModel), typeof(TViewModel), lifetime));
        return new NavigatRViewConfiguration(this, _services, typeof(TViewModel));
    }

    public NavigatRViewModelConfiguration RegisterView<TView, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TView : class
    {
        _services.Add(ServiceDescriptor.Describe(typeof(TView), typeof(TImplementation), lifetime));
        return new NavigatRViewModelConfiguration(this, _services, typeof(TImplementation));
    }

    public NavigatRViewModelConfiguration RegisterView<TView>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TView : class
    {
        _services.Add(ServiceDescriptor.Describe(typeof(TView), typeof(TView), lifetime));
        return new NavigatRViewModelConfiguration(this, _services, typeof(TView));
    }

    public NavigatRConfiguration RegisterViewToViewModel<TView, TViewModel>(ServiceLifetime lifetime = ServiceLifetime.Transient) 
        where TView : class
        where TViewModel : class, IViewModel
    {
        RegisterViewToViewModel(typeof(TView), typeof(TViewModel), lifetime);
        return this;
    }

    internal void RegisterViewToViewModel(Type view, Type viewModel, ServiceLifetime lifetime)
    {
        _viewModelDescriptors.Add(ServiceDescriptor.Describe(view, viewModel, lifetime));
    }
}

public sealed class NavigatRViewConfiguration
{
    private readonly Type _viewModel;
    private readonly NavigatRConfiguration _configuration;
    private readonly IServiceCollection _serviceCollection;

    public NavigatRViewConfiguration(NavigatRConfiguration configuration, IServiceCollection serviceCollection, Type viewModel)
    {
        _configuration = configuration;
        _serviceCollection = serviceCollection;
        _viewModel = viewModel;
    }

    public NavigatRConfiguration ToView<TView, TImplementation>(ServiceLifetime lifeTime = ServiceLifetime.Transient)
        where TView : class
    {
        _serviceCollection.Add(ServiceDescriptor.Describe(typeof(TView), typeof(TImplementation), lifeTime));
        _configuration.RegisterViewToViewModel(typeof(TImplementation), _viewModel, lifeTime);
        return _configuration;
    }

    public NavigatRConfiguration ToView<TView>(ServiceLifetime lifeTime = ServiceLifetime.Transient)
        where TView : class
    {
        _serviceCollection.Add(ServiceDescriptor.Describe(typeof(TView), typeof(TView), lifeTime));
        _configuration.RegisterViewToViewModel(typeof(TView), _viewModel, lifeTime);
        return _configuration;
    }
}

public sealed class NavigatRViewModelConfiguration
{
    private readonly Type _view;
    private readonly NavigatRConfiguration _configuration;
    private readonly IServiceCollection _serviceCollection;

    public NavigatRViewModelConfiguration(NavigatRConfiguration configuration, IServiceCollection serviceCollection, Type view)
    {
        _configuration = configuration;
        _serviceCollection = serviceCollection;
        _view = view;
    }
    public NavigatRConfiguration ToViewModel<TViewModel, TImplementation>(ServiceLifetime lifeTime = ServiceLifetime.Transient)
        where TViewModel : class, IViewModel
    {
        _serviceCollection.Add(ServiceDescriptor.Describe(typeof(TViewModel), typeof(TImplementation), lifeTime));
        _configuration.RegisterViewToViewModel(_view, typeof(TImplementation), lifeTime);
        return _configuration;
    }

    public NavigatRConfiguration ToViewModel<TViewModel>(ServiceLifetime lifeTime = ServiceLifetime.Transient)
        where TViewModel : class, IViewModel
    {
        _serviceCollection.Add(ServiceDescriptor.Describe(typeof(TViewModel), typeof(TViewModel), lifeTime));
        _configuration.RegisterViewToViewModel(_view, typeof(TViewModel), lifeTime);
        return _configuration;
    }
}
