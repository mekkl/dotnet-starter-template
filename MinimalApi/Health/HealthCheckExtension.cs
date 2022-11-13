using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MinimalApi.Health;

public static class HealthCheckExtension
{
    public static void AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<LiveCheck>("live")
            .AddCheck<DbHealthCheck>("DbHealth");
    }

    public static void UseHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new HealthCheckResponse
                    {
                        HealthStatus = report.Status,
                        HealthChecks = report.Entries.Select(check => new HealthCheck
                        {
                            Component = check.Key,
                            HealthStatus = check.Value.Status,
                            Description = check.Value.Description ?? string.Empty,
                            Duration = check.Value.Duration,
                        }),
                        Duration = report.TotalDuration,
                    };

                    context.Response.StatusCode = response.HealthStatus == HealthStatus.Healthy
                        ? (int)HttpStatusCode.OK
                        : (int)HttpStatusCode.ServiceUnavailable;

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            })
            .WithName("Health")
            .WithDisplayName("Health")
            .WithSummary("Get services health status")
            .WithDescription("Checks the utilized services health status")
            .WithOpenApi();
    }
}