using Microsoft.Extensions.DependencyInjection;

namespace NavigatR.Providers;

public class NavigatorProvider : INavigatorProvider
{
    private readonly IServiceProvider _serviceProvider;

    public NavigatorProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TNavigator GetNavigator<TNavigator>() where TNavigator : class, INavigator
        => _serviceProvider.GetRequiredService<TNavigator>();

    public INavigator GetDefaultNavigator()
        => _serviceProvider.GetRequiredService<Navigator>();
}