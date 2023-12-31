﻿using NavigatR;
using NavigatR.Services;
using NavigatR.Wrappers;

namespace Microsoft.Extensions.DependencyInjection;

public class NavigatRConfiguration
{
    private Type? _navigationWrapperTypeToRegister;

    private List<ServiceDescriptor> _viewModelDescriptors = new List<ServiceDescriptor>();

    internal NavigatRConfiguration ConfigureNavigatRLibrary(IServiceCollection services)
    {
        foreach (var viewModelDescriptor in _viewModelDescriptors)
        {
            services.AddTransient(viewModelDescriptor.ServiceType);
            services.AddTransient(viewModelDescriptor.ImplementationType!);
        }

        services.AddSingleton((serviceProvider) => new ViewProvider(_viewModelDescriptors, new ViewFactory(serviceProvider)));

        services.AddTransient<INavigator, Navigator>();

        _ = _navigationWrapperTypeToRegister is null
            ? services.AddTransient(typeof(INavigationWrapper<>), typeof(NavigationWrapper<>))
            : services.AddTransient(typeof(INavigationWrapper<>), _navigationWrapperTypeToRegister);

        return this;
    }

    public NavigatRConfiguration ConfigureNavigationWrapper(Type navigationWrapperType)
    {
        if (navigationWrapperType.IsAssignableFrom(typeof(INavigationWrapper<>)))
            throw new ArgumentException($"The argument must be of type {typeof(INavigationWrapper<>).Name}");

        if (!navigationWrapperType.IsGenericType)
            throw new ArgumentException("The navigation wrapper type must be generic");

        _navigationWrapperTypeToRegister = navigationWrapperType;

        return this;
    }

    public NavigatRConfiguration ConfigureViewToViewModel<TView, TViewModel>(ServiceLifetime lifetime = ServiceLifetime.Transient) 
        where TView : class
        where TViewModel : class, IViewModel
    {
        _viewModelDescriptors.Add(ServiceDescriptor.Describe(typeof(TView), typeof(TViewModel), lifetime));
        return this;
    }
}
