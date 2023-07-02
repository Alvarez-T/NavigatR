using NavigatR.WPF;

namespace Microsoft.Extensions.DependencyInjection;

public static class WpfNavigatRServicesExtensions
{
    public static NavigatRConfiguration UseWpfNavigationWrapper(this NavigatRConfiguration configuration)
    {
        return configuration.ConfigureNavigationWrapper(typeof(WpfNavigationWrapper<>));
    }
}
