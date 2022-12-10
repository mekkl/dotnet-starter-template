using System.IdentityModel.Tokens.Jwt;
using Application.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace MinimalApi.Auth;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        var clientId = context.User.Claims
            .FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (!Guid.TryParse(clientId, out var parsedMemberId))
        {
            return;
        }

        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var permissions = await mediator.Send(new GetPermissionsQuery(parsedMemberId));
        
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}