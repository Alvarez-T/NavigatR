namespace Microsoft.Extensions.DependencyInjection;

public static class NavigatRMvvmConfiguration
{
    public static IServiceCollection ConfigureViewToViewModel<TView, TViewModel>(this IServiceCollection services)
    {
        return services;
    }
}
