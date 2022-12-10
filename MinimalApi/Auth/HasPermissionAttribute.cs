using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace MinimalApi.Auth;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission) : base(policy: permission.ToString())
    {
    }
}