using NavigatR.Services;
using System.Windows.Controls;

namespace NavigatR.WPF.Controls;

public class NavigationContainer : ContentControl, INavigationContainer
{
    public NavigationContainer()
    {
        NavigationManager.AddContainer(this);
    }

    public void ShowView(object view)
    {
        Content = view;
    }
}
