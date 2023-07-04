using Microsoft.Extensions.DependencyInjection;
using NavigatR.MVVM;

namespace NavigatR.Tests;

public class FakeViewModel : INavigableViewModel
{

}
public class NavigableTests
{
    [SetUp]
    public void Setup()
    {
        //var t = new ServiceCollection();
        //t.AddNavigatR(null);
        //var provider = t.BuildServiceProvider();
        //var navigationWrapper = provider.GetRequiredService<INavigationWrapper>();

    }

    [Test]
    public void NavigatedTo()
    {
        
    }

}
