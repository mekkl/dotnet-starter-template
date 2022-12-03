using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Shared.Attributes;

namespace Application.Common.Behaviours;

[IgnoreCoverage]
public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Request: {Name} Anonymous {@Request}", requestName, request);
        
        return Task.CompletedTask;
    }
}