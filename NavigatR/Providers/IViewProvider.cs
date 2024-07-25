namespace NavigatR.Providers;

public interface IViewProvider
{
    public TView GetViewFromViewModel<TView>(IViewModel viewModel) where TView : class;
    public TView GetView<TView>() where TView : class;
}