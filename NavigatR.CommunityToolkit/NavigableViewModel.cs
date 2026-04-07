


using NavigatR.Providers;

namespace NavigatR.CommunityToolkit;

public abstract class NavigableViewModel : ObservableViewModel, INavigableViewModel
{
    protected INavigator Navigator { get; }

    protected NavigableViewModel()
    {
        Navigator = NavigatorLocator.GetDefaultNavigator();
    }

    protected virtual Task<bool> CanNavigate() => Task.FromResult(true);
    protected virtual Task OnNavigation() => Task.CompletedTask;
    Task<bool> INavigable.CanNavigate() => CanNavigate();
    Task INavigable.OnNavigation() => OnNavigation();
}