using Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace MinimalApi.Auth;

/// <summary>
/// Be aware of JWT size when utilizing it for permission storage. Also, JWT lifetime can have the effect that a
/// update of the user permissions not taking effect before the JWT is refreshed/revoked.
/// </summary>
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        var permissions = context.User.Claims
            .Where(claim => claim.Type == CustomClaims.Permissions)
            .Select(claim => claim.Value)
            .ToHashSet();
        
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}