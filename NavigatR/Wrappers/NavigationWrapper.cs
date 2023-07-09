using NavigatR.Services;

namespace NavigatR.Wrappers;

public class NavigationWrapper<T> : INavigationWrapper<T>
    where T : INavigable
{
    public virtual Task ExecuteNavigation(INavigable navigable)
    {
        return navigable.Navigate();
    }

    protected void ShowView(object view)
    {
        NavigationManager.GetContainer().ShowView(view);
    }
}
