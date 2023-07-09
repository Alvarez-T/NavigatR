namespace NavigatR.Services;

public static class NavigationManager
{
    private static List<INavigationContainer> _containers = new List<INavigationContainer>();

    internal static INavigationContainer GetContainer()
    {
        if (!_containers.Any())
            throw new InvalidOperationException("No NavigationContainer was located to display the View.");
        //later I will change this to support more than one Container.
        return _containers.First();
    }

    public static void AddContainer(INavigationContainer navigationContainer)
    {
        _containers.Add(navigationContainer);
    }
}
