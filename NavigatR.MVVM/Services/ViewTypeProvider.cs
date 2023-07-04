﻿namespace NavigatR.MVVM.Services;

public sealed class ViewProvider
{
    private readonly IViewFactory _viewFactory;

    private Dictionary<IViewModel, Type> _viewModelDictionary;

    public ViewProvider(IViewFactory viewFactory)
    {
        _viewFactory = viewFactory;
    }

    internal void AddViewAndViewModelAssociation(Type view, IViewModel viewModel)
    {
        if (!_viewModelDictionary.TryAdd(viewModel, view))
        {
            _viewModelDictionary[viewModel] = view;
        }
    }

    public TView GetViewFromViewModel<TView>(IViewModel viewModel) where TView : class
    {
        Type viewType;
        if (!_viewModelDictionary.TryGetValue(viewModel, out viewType!))
            throw new InvalidOperationException($"It was not found a View for the {viewModel.GetType().Name}. The View and View Model must be registered.");

        if (viewType != typeof(TView))
            throw new InvalidCastException($"The view is not a type of {typeof(TView).Name}.");

        return (TView)_viewFactory.CreateView(viewType);
    }
}

