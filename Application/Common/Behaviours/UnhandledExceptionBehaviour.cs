﻿using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Attributes;

namespace Application.Common.Behaviours;

[IgnoreCoverage]
[ExcludeFromCodeCoverage]
public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}