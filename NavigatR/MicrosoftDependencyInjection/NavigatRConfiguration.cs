using NavigatR;
using NavigatR.MVVM;
using NavigatR.MVVM.Services;

namespace Microsoft.Extensions.DependencyInjection;

public class NavigatRConfiguration
{
    public Type? NavigationWrapperTypeToRegister { get; private set; }

    private ViewProvider _viewProvider;
    private readonly IServiceCollection _services;

    public NavigatRConfiguration(IServiceCollection services)
    {
        _services = services;
    }

    internal NavigatRConfiguration ConfigureViewProvider()
    {
        _services.AddSingleton<ViewProvider>((serviceProvider) => _viewProvider = new ViewProvider(new ViewFactory(serviceProvider)));
        return this;
    }

    internal NavigatRConfiguration ConfigureNavigatRLibrary()
    {
        _services
            .AddTransient<INavigator, Navigator>()
            .AddTransient(typeof(INavigationWrapper<>), typeof(NavigationWrapper<>))
            .AddTransient<IViewFactory, ViewFactory>();

        _ = NavigationWrapperTypeToRegister is null
            ? _services.AddTransient(typeof(INavigationWrapper<>), typeof(NavigationWrapper<>))
            : _services.AddTransient(typeof(INavigationWrapper<>), NavigationWrapperTypeToRegister);

        return this;
    }

    public NavigatRConfiguration ConfigureNavigationWrapper(Type navigationWrapperType)
    {
        if (navigationWrapperType.IsAssignableFrom(typeof(INavigationWrapper<>)))
            throw new ArgumentException("The argument must be of type INavigationWrapper");

        if (!navigationWrapperType.IsGenericType)
            throw new ArgumentException("The navigation wrapper type must be generic");

        NavigationWrapperTypeToRegister = navigationWrapperType;

        return this;
    }

    public NavigatRConfiguration ConfigureViewToViewModel<TView, TViewModel>() 
        where TView : class
        where TViewModel : class, IViewModel
    {
        _services.AddTransient<TView>();
        _services.AddTransient<TViewModel>();
        _viewProvider.AddViewAndViewModelAssociation(typeof(TView), typeof(TViewModel));
        return this;
    }



}
