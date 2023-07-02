using Microsoft.Extensions.DependencyInjection;

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
        t.AddNavigatR(null);
        var provider = t.BuildServiceProvider();
        var navigator = new Navigator(provider);
        navigator.NavigateTo<FakeViewModel>();
    }
}
