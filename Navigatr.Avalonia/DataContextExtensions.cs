using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace NavigatR.Avalonia;

public static class DataContextExtensions
{
    public static void BindViewModelToTopLevelControl(this View view)
    {
        var topLevelControl = view.Content as Control;
        var binding = new Binding
        {
            Path = nameof(view.ViewModel),
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            Source = view
        };

        topLevelControl!.Bind(StyledElement.DataContextProperty, binding);
    }
}