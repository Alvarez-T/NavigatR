using System.Windows.Controls;

namespace NavigatR;

public class ShellNavigator : ContentControl, IShellNavigation
{
    public void ShowView(object view)
    {
        this.Content = view;
    }
}
