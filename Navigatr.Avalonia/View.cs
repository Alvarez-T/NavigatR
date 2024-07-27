using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace NavigatR.Avalonia;

public abstract partial class View : UserControl
{
    public static readonly StyledProperty<IViewModel> ViewModelProperty =
        AvaloniaProperty.Register<View, IViewModel>(nameof(ViewModel));

    public IViewModel? ViewModel
    {
        get => GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
}

