using System.Drawing;

namespace NavigatR;

internal sealed class Navigator : INavigator
{
    private readonly IServiceProvider _serviceProvider;

    public Navigator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void NavigateFoward(int? index = null)
    {

    }

    public void NavigateFowardTo<T>() where T : INavigable
    {

    }

    public void NavigateBackward(int? index = null)
    {

    }

    public void NavigateBackwardTo<T>() where T: INavigable
    {

    }

    public Task NavigateTo<T>(object? parameter = null) where T : INavigable
    {
        throw new NotImplementedException();
    }

    public Task NavigateTo<T, TParam>(TParam parameter) where T : INavigable<TParam>
    {
        throw new NotImplementedException();
    }
}

