namespace NavigatR;

public sealed class Navigator : INavigator
{
    private readonly IServiceProvider _serviceProvider;

    public INavigation NavPane { get; set; }

    public Navigator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void NavigateForward(int? index = null)
    {
        throw new NotImplementedException();
    }

    public void NavigateForwardTo<T>() where T : INavigable
    {
        throw new NotImplementedException();
    }

    public void NavigateBackward(int? index = null)
    {
        throw new NotImplementedException();
    }

    public void NavigateBackwardTo<T>() where T: INavigable
    {
        throw new NotImplementedException();
    }

    public void NavigateTo(INavigable navigable)
    {
        throw new NotImplementedException();
    }

    public void NavigateTo<T>(object? parameter = null) where T : INavigable
    {
        throw new NotImplementedException();
    }
}

