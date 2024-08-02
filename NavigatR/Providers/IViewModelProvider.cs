namespace NavigatR.Providers;

public interface IViewModelProvider
{
    public TViewModel GetViewModelFromView<TViewModel>(Type view) where TViewModel : IViewModel;
    public IViewModel GetViewModelFromView(Type view);
    public TViewModel GetViewModel<TViewModel>() where TViewModel : IViewModel;
}