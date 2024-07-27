using NavigatR.Exceptions;

namespace NavigatR.Providers;

public static class ViewLocator
{
    private static IViewProvider? _viewProvider;

    public static void CreateLocator(IViewProvider viewProvider)
    {
        if (_viewProvider is not null)
            throw new LocatorAlreadyRegisteredException(nameof(ViewModelLocator));

        _viewProvider = viewProvider;
    }

    public static TView LocateView<TView>(IViewModel viewModel) where TView : class
        => _viewProvider?.GetView<TView>() ?? throw new LocatorNotRegisteredException(nameof(ViewModelLocator));


    public static TView LocateViewFromViewModel<TView>(IViewModel viewModel) where TView : class
            => _viewProvider?.GetViewFromViewModel<TView>(viewModel) ?? throw new LocatorNotRegisteredException(nameof(ViewModelLocator));
}