namespace NavigatR;

public class NavigationWrapper<T> : INavigationWrapper<T>
    where T : INavigable
{
    public virtual Task ExecuteNavigation(INavigable navigable)
    {
        return navigable.Navigate();
    }
}
