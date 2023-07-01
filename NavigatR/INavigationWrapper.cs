namespace NavigatR;

public interface INavigationWrapper<T>
    where T : INavigable
{
    Task ExecuteNavigation(INavigable navigable);
}
