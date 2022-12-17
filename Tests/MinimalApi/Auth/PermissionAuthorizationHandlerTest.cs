using System.Security.Claims;
using Domain.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Auth;

namespace Tests.MinimalApi.Auth;

public class PermissionAuthorizationHandlerTest
{
    [Fact]
    public void Handle_WhenCalledWithPermission_ShouldSucceed()
    {
        const string readPermission = "read";
        var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new(CustomClaims.Permissions, readPermission ) }));
        var requirement = new PermissionRequirement(readPermission);

        var handlerContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, null);

        var sut = new PermissionAuthorizationHandler();
        sut.HandleAsync(handlerContext);

        handlerContext.HasSucceeded.Should().BeTrue();
    }
    
    [Fact]
    public void Handle_WhenCalledWithOtherPermission_ShouldNotSucceed()
    {
        const string readPermission = "read";
        var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new(CustomClaims.Permissions, "other" ) }));
        var requirement = new PermissionRequirement(readPermission);

        var handlerContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, null);

        var sut = new PermissionAuthorizationHandler();
        sut.HandleAsync(handlerContext);

        handlerContext.HasSucceeded.Should().BeFalse();
    }
    
    [Fact]
    public void Handle_WhenCalledWithoutPermission_ShouldNotSucceed()
    {
        const string readPermission = "read";
        var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()));
        var requirement = new PermissionRequirement(readPermission);

        var handlerContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, null);

        var sut = new PermissionAuthorizationHandler();
        sut.HandleAsync(handlerContext);

        handlerContext.HasSucceeded.Should().BeFalse();
    }
    
    [Fact]
    public void Handle_WhenCalledWithoutIdentityPermission_ShouldNotSucceed()
    {
        const string readPermission = "read";
        var user = new ClaimsPrincipal();
        var requirement = new PermissionRequirement(readPermission);

        var handlerContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, null);

        var sut = new PermissionAuthorizationHandler();
        sut.HandleAsync(handlerContext);

        handlerContext.HasSucceeded.Should().BeFalse();
    }
}