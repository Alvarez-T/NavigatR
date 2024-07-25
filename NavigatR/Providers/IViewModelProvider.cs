namespace NavigatR.Providers;

public interface IViewModelProvider
{
    public TViewModel GetViewModelFromView<TViewModel>(Type view) where TViewModel : IViewModel;
    public TViewModel GetViewModel<TViewModel>() where TViewModel : IViewModel;
}