using NavigatR;
using NavigatR.MVVM;

namespace Microsoft.Extensions.DependencyInjection;

public static class NavigatRExtensions
{
    public static IServiceCollection UseNavigatR(this IServiceCollection services, Action<NavigatRConfiguration> navigatrConfiguration)
    {
        var configuration = new NavigatRConfiguration(services);
        configuration.ConfigureViewProvider();

        navigatrConfiguration(configuration);

        configuration.ConfigureNavigatRLibrary();
        return services;
    }
}
