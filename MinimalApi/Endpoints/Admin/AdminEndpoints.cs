using Application.Admin.Queries;
using MediatR;

namespace MinimalApi.Endpoints.Admin;

public class AdminEndpoints : IEndpointModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/admin");
        
        group.MapGet("/servertime", (IMediator mediator) => mediator.Send(new GetServerTimeQuery()))
            .WithName("Server Time")
            .WithDisplayName("Server Time")
            .WithSummary("Display server time")
            .WithDescription("Get the current server time")
            .WithOpenApi();
    }
}