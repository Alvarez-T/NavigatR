using NavigatR.Services;
using System.Windows.Controls;

namespace NavigatR.WPF;

public class NavPane : ContentControl, INavigation
{
    public NavPane()
    {
        NavigationManager.AddContainer(this);
    }

    public void PerformNavigation(object view)
    {
        Content = view;
    }
}
