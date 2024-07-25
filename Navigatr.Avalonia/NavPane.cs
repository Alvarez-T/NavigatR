using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using NavigatR.Providers;

namespace NavigatR.Avalonia
{
    public class NavPane : ContentControl, INavigation
    {
        public static readonly StyledProperty<INavigator> NavigatorProperty =
            AvaloniaProperty.Register<NavPane, INavigator>(nameof(Navigator),
                defaultValue: NavigatorLocator.GetDefaultNavigator());

        public INavigator Navigator
        {
            get => GetValue(NavigatorProperty);
            set => SetValue(NavigatorProperty, value);
        }

        public void PerformNavigation(object view)
        {
            Content = view;
        }

        public virtual void OnNavigationDenied() { }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            Navigator.NavPane = this;
        }
    }
}
