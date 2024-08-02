using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using NavigatR.Exceptions;

namespace NavigatR.Providers;

public class ViewModelLocator
{
    private static IViewModelProvider? _viewModelProvider;

    internal static void CreateLocator(IViewModelProvider viewModelProvider)
    {
        if (_viewModelProvider is not null)
            throw new LocatorAlreadyRegisteredException(nameof(ViewModelLocator));

        _viewModelProvider = viewModelProvider;
    }

    public static TViewModel LocateViewModel<TViewModel>() where TViewModel : IViewModel
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        {
            return default!;
        }

        if (_viewModelProvider is null)
            throw new LocatorNotRegisteredException(nameof(ViewModelLocator));

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
            throw new LocatorNotRegisteredException(nameof(ViewModelLocator));

        return _viewModelProvider.GetViewModelFromView<TViewModel>(typeof(TView));
    }

    public static IViewModel LocateViewModelFromView(Type viewType)
    {
        if (_viewModelProvider is null)
            throw new LocatorNotRegisteredException(nameof(ViewModelLocator));

        return _viewModelProvider.GetViewModelFromView(viewType);
    }


}

