using Application.Common.Interfaces.Auth;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Auth;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();
        
        services.AddSingleton<IDbConnectionFactory, AppDbConnectionFactory>();
        
        services.AddTransient<IMemberRepository, MemberRepository>();
        
        services.AddTransient<DbContext, AppDbContext>();
        services.AddDbContext<AppDbContext>();
        services.AddScoped<AppDbContextInitializer>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}