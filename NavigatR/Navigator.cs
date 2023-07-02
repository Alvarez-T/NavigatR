using Microsoft.Extensions.DependencyInjection;
using System.Drawing;

namespace NavigatR;

public sealed class Navigator : INavigator
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
        var wrapper = _serviceProvider.GetRequiredService<INavigationWrapper<T>>();
        var navigable = (INavigable)Activator.CreateInstance(typeof(T))!;
        wrapper.ExecuteNavigation(navigable);
        return Task.CompletedTask;
    }

    public Task NavigateTo<T, TParam>(TParam parameter) where T : INavigable<TParam>
    {
        throw new NotImplementedException();
    }
}

