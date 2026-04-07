using NavigatR.Providers;

namespace NavigatR;

public sealed class Navigator : INavigator
{
    private readonly IViewProvider _viewProvider;

    public INavigation? NavPane { get; set; }

    public Navigator(IViewProvider viewProvider)
    {
        _viewProvider = viewProvider;
    }

    public void NavigateForward(int? index = null)
    {
        throw new NotImplementedException();
    }

    public void NavigateForwardTo<T>() where T : class, INavigable
    {
        throw new NotImplementedException();
    }

    public void NavigateBackward(int? index = null)
    {
        throw new NotImplementedException();
    }

    public void NavigateBackwardTo<T>() where T: class, INavigable
    {
        throw new NotImplementedException();
    }

    public void NavigateTo(INavigable navigable)
    {
        NavPane?.PerformNavigation(navigable);
    }

    public void NavigateTo<T>(object? parameter = null) where T : class, INavigable
    {
        object view = _viewProvider.GetViewFromViewModel<T>();
        NavPane?.PerformNavigation(view);
    }
}

