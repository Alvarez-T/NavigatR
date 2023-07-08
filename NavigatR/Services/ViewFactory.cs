using Microsoft.Extensions.DependencyInjection;

namespace NavigatR.Services;

internal sealed class ViewFactory : IViewFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ViewFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object CreateView(Type type)
    {
        return _serviceProvider.GetRequiredService(type);
    }
}

