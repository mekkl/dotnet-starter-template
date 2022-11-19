using Application.Common.Interfaces.Adapters;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Adapters;

public class LoggerAdapter<T> : ILoggerAdapter<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogDebug(string message)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug(message);
    }

    public void LogInformation(string message)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            _logger.LogInformation(message);
    }

    public void LogInformation<T0>(string message, T0 arg0)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            _logger.LogInformation(message, arg0);
    }

    public void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1)
    {
        if (_logger.IsEnabled(LogLevel.Information))
            _logger.LogInformation(message, arg0, arg1);
    }

    public void LogWarning(string message)
    {
        if (_logger.IsEnabled(LogLevel.Warning))
            _logger.LogWarning(message);
    }

    public void LogWarning(Exception exception, string message)
    {
        if (_logger.IsEnabled(LogLevel.Warning))
            _logger.LogWarning(exception, message);
    }

    public void LogError(string message)
    {
        if (_logger.IsEnabled(LogLevel.Error))
            _logger.LogError(message);
    }

    public void LogError(Exception exception, string message)
    {
        if (_logger.IsEnabled(LogLevel.Error))
            _logger.LogError(exception, message);
    }

    public void LogError<T0>(Exception exception, string message, T0 arg0)
    {
        if (_logger.IsEnabled(LogLevel.Error))
            _logger.LogError(exception, message, arg0);
    }
}