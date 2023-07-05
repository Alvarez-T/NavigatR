using Microsoft.Extensions.DependencyInjection;
using NavigatR.WPF;
using System.Windows;

namespace NavigatR.Tests;

public class FakeViewModel : INavigableViewModel
{

}

public class Test : FrameworkElement
{

}

internal class NavigatorTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void TestNavigation()
    {
        var t = new ServiceCollection();
        t.UseNavigatR(configuration =>
        {
            configuration.UseWpfNavigationWrapper()
            .ConfigureViewToViewModel<Test, FakeViewModel>();
        });
        var provider = t.BuildServiceProvider();
        var navigator = new Navigator(provider);
        navigator.NavigateTo<FakeViewModel>();
    }
}
