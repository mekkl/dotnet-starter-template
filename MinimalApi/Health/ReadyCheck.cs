using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MinimalApi.Health;

public class ReadyCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        return Task.FromResult(HealthCheckResult.Healthy("Ready"));
    }
}