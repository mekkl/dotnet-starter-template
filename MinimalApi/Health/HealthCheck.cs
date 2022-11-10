using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MinimalApi.Health;

public record HealthCheck
{
    public HealthStatus HealthStatus { get; set; }
    public string Component { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public Exception? Exception { get; set; }
}