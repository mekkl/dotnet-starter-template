using Application.Common.Interfaces.Persistence;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnectionFactory, AppDbConnectionFactory>();
        services.AddDbContext<DbContext, AppDbContext>();
        services.AddDbContext<AppDbContext>();
        services.AddScoped<AppDbContextInitializer>();
        
        return services;
    }
}