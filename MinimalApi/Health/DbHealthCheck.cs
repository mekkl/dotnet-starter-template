using Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MinimalApi.Health;

public class DbHealthCheck : IHealthCheck
{
    private readonly IUnitOfWork _unitOfWork;

    public DbHealthCheck(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        await _unitOfWork.UserRepository.ListAsync(cancellationToken);
        return HealthCheckResult.Healthy("Successful call to db");
    }
}