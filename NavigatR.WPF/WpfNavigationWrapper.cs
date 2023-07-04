using NavigatR.MVVM;
using NavigatR.MVVM.Services;
using System.Windows;

namespace NavigatR.WPF;
public class WpfNavigationWrapper<T> : INavigationWrapper<T>
    where T : INavigable
{
    private readonly ViewProvider _viewProvider;

    public WpfNavigationWrapper(ViewProvider viewTypeProvider)
    {
        _viewProvider = viewTypeProvider;
    }

    public Task ExecuteNavigation(INavigable navigable)
    {
        if (navigable is IViewModel viewModel)
        {
            var view = _viewProvider.GetViewFromViewModel<FrameworkElement>(viewModel);
            view.DataContext = viewModel;
        }
        else
            navigable.Navigate();


        return Task.CompletedTask;
    }
}