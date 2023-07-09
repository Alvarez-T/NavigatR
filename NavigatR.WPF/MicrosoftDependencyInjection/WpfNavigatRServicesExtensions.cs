using NavigatR;
using NavigatR.WPF.Controls;
using NavigatR.Wrappers;

namespace Microsoft.Extensions.DependencyInjection;

public static class WpfNavigatRServicesExtensions
{
    public static NavigatRConfiguration UseWpfNavigationWrapper(this NavigatRConfiguration configuration)
    {
        return configuration.ConfigureNavigationWrapper(typeof(WpfNavigationWrapper<>));
    }
}
