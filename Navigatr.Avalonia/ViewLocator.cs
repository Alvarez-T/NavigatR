using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace NavigatR.Avalonia;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is not IViewModel viewModel)
            throw new NotImplementedException("The ViewLocator only works with the IViewModel interfaces");

        return Providers.ViewLocator.LocateViewFromViewModel<Control>((IViewModel)viewModel);
    }

    public bool Match(object? data) => data is IViewModel;
}