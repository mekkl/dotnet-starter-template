using Application.Auth.Commands;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Auth;

namespace MinimalApi.Endpoints.Auth;

public class AuthEndpoints : IEndpointModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");

        group.MapGet("/debug",  [Permission(Permission.ReadMember)] () => "OK")
            .WithName("Auth Debug")
            .WithDisplayName("Auth Debug")
            .WithSummary("Auth Debug endpoint")
            .WithDescription("Test auth related code")
            .WithOpenApi();
        
        group.MapPost("/login", (IMediator mediator, [FromBody] LoginCommand loginCommand) 
                => mediator.Send(loginCommand))
            .WithName("Login")
            .WithDisplayName("Login")
            .WithSummary("Login user")
            .WithDescription("Authenticates and login the member")
            .WithOpenApi();
    }
}