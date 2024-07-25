using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace NavigatR.Avalonia;

public abstract class View : UserControl
{
    public static readonly StyledProperty<IViewModel> ViewModelProperty =
        AvaloniaProperty.Register<View, IViewModel>(nameof(ViewModel));

    public IViewModel ViewModel
    {
        get => GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public new object? DataContext
    {
        get { return base.DataContext; }
        set
        {
            //if (ReferenceEquals(ViewModel, DataContext))
                base.DataContext = value;
        }
    }

    static View()
    {
        //ViewModelProperty.Changed.AddClassHandler<View>((view, args) =>
        //{
        //    view.DataContext = args.NewValue;
        //});
    }

    protected void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SetTopLevelDataContext()
    {
        // Find the top-level control
        var topLevelControl = GetTopLevelControl();
        if (topLevelControl != null)
        {
            // Set the DataContext
            topLevelControl.DataContext = ViewModel;
        }
    }

    private Control GetTopLevelControl()
    {
        // Traverse up the visual tree to find the top-level control
        Control current = this;
        while (current.GetVisualParent() is not null)
        {
            current = (Control)current.GetVisualParent()!;
        }

        return current as Control;
    }

    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnAttachedToLogicalTree(e);
        
       // DataContext = ViewModel;
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
       
    }
}

