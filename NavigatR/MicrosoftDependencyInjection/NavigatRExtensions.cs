using NavigatR.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class NavigatRExtensions
{
    public static IServiceCollection UseNavigatR(this IServiceCollection services, Action<NavigatRConfiguration> navigatrConfiguration)
    {
        var configuration = new NavigatRConfiguration();

        navigatrConfiguration(configuration);

        configuration.ConfigureNavigatRLibrary(services);
        services.AddHostedService<NavigatRService>();
        return services;
    }
}
