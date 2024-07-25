namespace NavigatR;

public interface INavigator
{
    INavigation NavPane { get; set; }
    void NavigateBackward(int? index = null);
    void NavigateBackwardTo<T>() where T : INavigable;
    void NavigateForward(int? index = null);
    void NavigateForwardTo<T>() where T : INavigable;
    void NavigateTo(INavigable navigable);
    void NavigateTo<T>(object? parameter = null) where T : INavigable;
}
