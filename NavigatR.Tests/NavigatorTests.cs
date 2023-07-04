using Microsoft.Extensions.DependencyInjection;
using NavigatR.WPF;

namespace NavigatR.Tests;


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
        t.AddTransient<INavigationWrapper<FakeViewModel>, WpfNavigationWrapper<FakeViewModel>>();
        t.UseNavigatR(null);
        var provider = t.BuildServiceProvider();
        var navigator = new Navigator(provider);
        navigator.NavigateTo<FakeViewModel>();
    }
}
