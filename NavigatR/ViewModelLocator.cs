﻿using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace NavigatR;

public class ViewModelLocator
{
    private static IServiceProvider _services;

    internal static void CreateViewModelLocator(IServiceProvider serviceProvider)
    {
        if (_services is not null)
            throw new InvalidOperationException("The ViewModelLocator is already registered");

        _services = serviceProvider;
    }

    public static TViewModel LocateViewModel<TViewModel>() where TViewModel : IViewModel
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        {
            return default(TViewModel)!;
        }

        if (_services is null)
            throw new NullReferenceException("The ViewModelLocator was not registered");

        return _services.GetRequiredService<TViewModel>();
    }

}
