using System.Net;
using System.Text.Json;
using Application.Common.Interfaces.Adapters;
using Shared.Attributes;

namespace MinimalApi.Middleware;

[IgnoreCoverage]
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IHostEnvironment hostEnvironment, ILoggerAdapter<ErrorHandlerMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;

            var errorResponse = ex switch
            {
                _ => CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error")
            };

            response.ContentType = "application/json";
            response.StatusCode = errorResponse.Code;

            if (errorResponse.Code == (int)HttpStatusCode.InternalServerError)
            {
                string path = context.Request.Path;
                logger.LogError(ex, "Unhandled exception for request path={Path}", path);
            }

            if (hostEnvironment.IsDevelopment() || hostEnvironment.IsEnvironment("Local"))
            {
                errorResponse.Reason = ex.Message;
                errorResponse.Note = "See log for stacktrace information";
            }

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }

    private static ErrorResponse CreateErrorResponse(HttpStatusCode code, string status, string? reason = null, string? note = null)
    {
        return new ErrorResponse()
        {
            Code = (int)code,
            Status = status,
            Reason = reason,
            Note = note,
        };
    }

    private record ErrorResponse
    {
        public int Code { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Reason { get; set; }
        public string? Note { get; set; }
    }
}

public static class ErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}