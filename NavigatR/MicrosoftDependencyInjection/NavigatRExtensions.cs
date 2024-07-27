using NavigatR.Exceptions;
using NavigatR.Providers;
using NavigatR.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class NavigatRExtensions
{
    public static IServiceCollection UseNavigatR(this IServiceCollection services, Action<NavigatRConfiguration> navigatrConfiguration)
    {
        var configuration = new NavigatRConfiguration(services);

        navigatrConfiguration(configuration);

        configuration.ConfigureNavigatRLibrary();
        return services;
    }

    public static void ConfigureLocators(this IServiceProvider provider)
    {
        if (provider.GetService<IViewProvider>() is null)
            throw new NavigatRNotConfiguredException();

        ViewLocator.CreateLocator(provider.GetRequiredService<IViewProvider>());
        ViewModelLocator.CreateLocator(provider.GetRequiredService<IViewModelProvider>());
        NavigatorLocator.CreateLocator(provider.GetRequiredService<INavigatorProvider>());
    }
}
