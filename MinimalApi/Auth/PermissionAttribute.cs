using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace MinimalApi.Auth;

public sealed class PermissionAttribute : AuthorizeAttribute
{
    public PermissionAttribute(Permission permission) : base(policy: permission.ToString())
    {
    }
}