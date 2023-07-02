using NavigatR;

namespace Microsoft.Extensions.DependencyInjection;

public class NavigatRConfiguration
{
    public Type? NavigationWrapperTypeToRegister { get; private set; }

    public NavigatRConfiguration ConfigureNavigatRLibrary(IServiceCollection services)
    {
        services.AddTransient<INavigator, Navigator>();
        services.AddTransient(typeof(INavigationWrapper<>), typeof(NavigationWrapper<>));
        return this;
    }

    public NavigatRConfiguration ConfigureNavigationWrapper(Type navigationWrapperType)
    {
        if (navigationWrapperType != typeof(INavigationWrapper<>))
            throw new ArgumentException("The argument must be of type INavigationWrapper");

        if (!navigationWrapperType.IsGenericType)
            throw new ArgumentException("The navigation wrapper type must be generic");

        NavigationWrapperTypeToRegister = navigationWrapperType;

        return this;
    }
}
