namespace NavigatR.Providers;

public static class NavigatorLocator
{
    private static INavigatorProvider? _navigatorProvider;

    public static void CreateNavigatorLocator(INavigatorProvider navigatorProvider)
    {
        if (_navigatorProvider is not null)
            throw new InvalidOperationException("The NavigatorLocator is already created");

        _navigatorProvider = navigatorProvider;
    }

    public static TNavigator GetNavigator<TNavigator>() where TNavigator : class, INavigator
        => _navigatorProvider?.GetNavigator<TNavigator>() ?? throw new NullReferenceException("The NavigatorLocator was not registered");

    public static INavigator GetDefaultNavigator()
        => _navigatorProvider?.GetDefaultNavigator() ?? throw new NullReferenceException("The NavigatorLocator was not registered");
}