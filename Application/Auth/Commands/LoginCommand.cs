using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces.Auth;
using MediatR;
using Shared.Attributes;

namespace Application.Auth.Commands;

[IgnoreCoverage]
[ExcludeFromCodeCoverage]
public record LoginCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

[IgnoreCoverage]
[ExcludeFromCodeCoverage]
public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_jwtProvider.GenerateJwtToken("clientId"));
    }
}