using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace NavigatR.Providers;

public class ViewModelLocator
{
    private static IViewModelProvider? _viewModelProvider;

    internal static void CreateViewModelLocator(IViewModelProvider viewModelProvider)
    {
        if (_viewModelProvider is not null)
            throw new InvalidOperationException("The ViewModelLocator is already registered");

        _viewModelProvider = viewModelProvider;
    }

    public static TViewModel LocateViewModel<TViewModel>() where TViewModel : IViewModel
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        {
            return default!;
        }

        if (_viewModelProvider is null)
            throw new NullReferenceException("The ViewModelLocator was not registered");

        return _viewModelProvider.GetViewModel<TViewModel>();
    }

    public static TViewModel LocateViewModelFromView<TViewModel, TView>()
        where TViewModel : IViewModel
        where TView : class
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        {
            return default!;
        }

        if (_viewModelProvider is null)
            throw new NullReferenceException("The ViewModelLocator was not registered");

        return _viewModelProvider.GetViewModelFromView<TViewModel>(typeof(TView));
    }
}

