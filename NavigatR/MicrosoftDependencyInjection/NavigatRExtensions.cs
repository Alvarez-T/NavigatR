using NavigatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class NavigatRExtensions
{
    public static IServiceCollection AddNavigatR(this IServiceCollection services, Action<NavigatRConfiguration> navigatrConfiguration)
    {
        services.AddTransient(typeof(INavigationWrapper<>), typeof(NavigationWrapper<>));
        return services;
    }
}
