using System.Reflection;
using Domain.Enums;
using MinimalApi.Auth;

namespace MinimalApi.Endpoints;

public static class EndpointsExtension
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var endpointModules = types.Where(t =>
                !t.IsAbstract &&
                typeof(IEndpointModule).IsAssignableFrom(t)
                && t.IsPublic
            );
        
        foreach (var endpointModule in endpointModules)
        {
            services.AddSingleton(typeof(IEndpointModule), endpointModule);
        }
        
        return services;
    }
    
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        foreach (var endpointModule in builder.ServiceProvider.GetServices<IEndpointModule>())
        {
            endpointModule.AddRoutes(builder);
        }

        return builder;
    }
    
    public static TBuilder RequireAuthorization<TBuilder>(this TBuilder builder, params Permission[] permissions) where TBuilder : IEndpointConventionBuilder
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (permissions == null)
        {
            throw new ArgumentNullException(nameof(permissions));
        }

        return builder.RequireAuthorization(permissions
            .Select(permission => new PermissionAttribute(permission)).ToArray());
    }
}