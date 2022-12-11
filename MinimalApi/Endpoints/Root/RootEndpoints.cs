namespace MinimalApi.Endpoints.Root;

public class RootEndpoints : IEndpointModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("").RequireAuthorization();
        
        group.MapGet("/ping", () => "pong")
            .WithName("Ping")
            .WithDisplayName("Ping")
            .WithSummary("Ping pong endpoint")
            .WithDescription("Get a pong response from server")
            .WithOpenApi();
    }
}