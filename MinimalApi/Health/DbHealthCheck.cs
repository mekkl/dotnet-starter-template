using Application.Common.Interfaces.Persistence;
using Domain.Model;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MinimalApi.Health;

public class DbHealthCheck : IHealthCheck
{
    private readonly IUserRepository _userRepository;

    public DbHealthCheck(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        await _userRepository.ListAsync();
        return HealthCheckResult.Healthy("Successful call to db");
    }
}