using Microsoft.Extensions.Hosting;
using NavigatR;

namespace NavigatR.Services;

internal class NavigatRService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public NavigatRService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ViewModelLocator.CreateViewModelLocator(_serviceProvider);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
