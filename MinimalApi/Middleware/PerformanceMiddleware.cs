using System.Diagnostics;
using Application.Common.Interfaces.Adapters;

namespace MinimalApi.Middleware;

public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;

    public PerformanceMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILoggerAdapter<PerformanceMiddleware> logger)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        finally
        {
            var path = context.Request.Path;
            stopwatch.Stop();
            logger.LogInformation("Processed request in ms={Ms} path={Path}", stopwatch.ElapsedMilliseconds, path);
        }
    }
}

public static class PerformanceMiddlewareExtensions
{
    public static IApplicationBuilder UsePerformanceMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PerformanceMiddleware>();
    }
}