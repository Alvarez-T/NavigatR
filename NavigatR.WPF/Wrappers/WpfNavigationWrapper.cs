using NavigatR.Services;
using System.Windows;

namespace NavigatR.Wrappers;
public class WpfNavigationWrapper<T> : NavigationWrapper<T>
    where T : INavigable
{
    private readonly ViewProvider _viewProvider;

    public WpfNavigationWrapper(IShellNavigation shellNavigation, ViewProvider viewTypeProvider) : base(shellNavigation)
    {
        _viewProvider = viewTypeProvider;
    }

    public override Task ExecuteNavigation(INavigable navigable)
    {
        if (navigable is IViewModel viewModel)
        {
            var view = _viewProvider.GetViewFromViewModel<FrameworkElement>(viewModel);
            view.DataContext = viewModel;
            ShowView(view);
        }
        else
            base.ExecuteNavigation(navigable);


        return Task.CompletedTask;
    }
}