namespace NavigatR.Providers;

public static class ViewLocator
{
    private static IViewProvider? _viewProvider;

    public static void CreateViewLocator(IViewProvider viewProvider)
    {
        if (_viewProvider is not null)
            throw new InvalidOperationException("The ViewModelLocator is already registered");

        _viewProvider = viewProvider;
    }

    public static TView LocateView<TView>(IViewModel viewModel) where TView : class
        => _viewProvider?.GetView<TView>() ?? throw new NullReferenceException("The ViewLocator was not registered");


    public static TView LocateViewFromViewModel<TView>(IViewModel viewModel) where TView : class
            => _viewProvider?.GetViewFromViewModel<TView>(viewModel) ?? throw new NullReferenceException("The ViewLocator was not registered");
}