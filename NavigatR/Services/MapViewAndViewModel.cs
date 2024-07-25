using System.Collections.Concurrent;
using System.Collections.Immutable;
using Microsoft.Extensions.DependencyInjection;

namespace NavigatR.Services;

public class MapViewAndViewModel
{
    private readonly ImmutableDictionary<Type, Type> _forward;
    private readonly ImmutableDictionary<Type, Type> _reverse;

    public MapViewAndViewModel(ICollection<ServiceDescriptor> descriptors)
    {
        var forwardBuilder = ImmutableDictionary.CreateBuilder<Type, Type>();
        var reverseBuilder = ImmutableDictionary.CreateBuilder<Type, Type>();

        // Populate the builders
        foreach (var descriptor in descriptors)
        {
            forwardBuilder[descriptor.ServiceType] = descriptor.ImplementationType!;
            reverseBuilder[descriptor.ImplementationType!] = descriptor.ServiceType;
        }

        // Convert builders to immutable dictionaries
        _forward = forwardBuilder.ToImmutable();
        _reverse = reverseBuilder.ToImmutable();
    }

    public Type TryGetViewModelTypeByView(Type view)
    {
        if (!_forward.TryGetValue(view, out var viewModel))
            throw new InvalidOperationException($"The ViewModel was not found for the View \"{view.Name}\". Ensure the View and ViewModel are registered and linked together.");

        return viewModel;
    }

    public Type TryGetViewTypeByViewModel(Type viewModel)
    {
        if (!_reverse.TryGetValue(viewModel, out var view))
            throw new InvalidOperationException($"A View was not found for the ViewModel \"{viewModel.GetType().Name}\". Ensure the View and ViewModel are registered and linked together.");

        return view;
    } 
}