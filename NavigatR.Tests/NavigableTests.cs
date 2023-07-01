using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace NavigatR.Tests;

public class FakeViewModel : INavigable
{

}

public class FakeNavigator : INavigator
{
    private readonly INavigationWrapper _navigationWrapper;

    public FakeNavigator(INavigationWrapper navigationWrapper)
    {
        _navigationWrapper = navigationWrapper;
    }

    public void NavigateBackward(int? index = null)
    {
        throw new NotImplementedException();
    }

    public void NavigateBackwardTo<T>() where T : INavigable
    {
        throw new NotImplementedException();
    }

    public void NavigateFoward(int? index = null)
    {
        throw new NotImplementedException();
    }

    public void NavigateFowardTo<T>() where T : INavigable
    {
        throw new NotImplementedException();
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

public class NavigableTests
{
    [SetUp]
    public void Setup()
    {
        var t = new ServiceCollection();
        t.AddNavigatR(null);
        var provider = t.BuildServiceProvider();
        var navigationWrapper = provider.GetRequiredService<INavigationWrapper>();

    }

    [Test]
    public void NavigatedTo()
    {
        
    }

}
