using Shared.Attributes;
using Shared.Common;

namespace MinimalApi.HostedServices;

[IgnoreCoverage]
public class TimedHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public TimedHostedService(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => DoWork(cancellationToken), cancellationToken);
        return Task.CompletedTask;
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var logger = _serviceProvider.GetRequiredService<ILogger<TimedHostedService>>();
        const int seconds = 60;
        
        while (!cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Hello world every {Seconds} seconds!", seconds);
            await Delay.Seconds(seconds);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}