namespace NavigatR;

public interface INavigation
{
    INavigator Navigator { get; set; }
    void PerformNavigation(object view);
    void OnNavigationDenied();
}

