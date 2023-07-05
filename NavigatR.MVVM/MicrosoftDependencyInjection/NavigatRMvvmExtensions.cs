using NavigatR.MVVM;
using System.Net.Security;

namespace Microsoft.Extensions.DependencyInjection;

public static class NavigatRMvvmExtensions
{
    public static NavigatRMvvmConfiguration UseMvvm(this NavigatRConfiguration configuration)
    {
        return new NavigatRMvvmConfiguration(configuration);
    }

    public static NavigatRMvvmConfiguration ConfigureViewToViewModel<TView, TViewModel>(this NavigatRMvvmConfiguration navigatrMvvmConfiguration)
        where TView : class
        where TViewModel : IViewModel
    {
        return navigatrMvvmConfiguration;
    }
}

public class NavigatRMvvmConfiguration : NavigatRConfiguration
{
    private readonly NavigatRConfiguration _navigatRConfiguration;

    public NavigatRMvvmConfiguration(NavigatRConfiguration navigatRConfiguration)
    {
        _navigatRConfiguration = navigatRConfiguration;
    }
}

