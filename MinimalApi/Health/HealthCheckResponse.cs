using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MinimalApi.Health;

public record HealthCheckResponse
{
    public HealthStatus HealthStatus { get; set; }
    public IEnumerable<HealthCheck> HealthChecks { get; set; } = new List<HealthCheck>();
    public TimeSpan Duration { get; set; }
}