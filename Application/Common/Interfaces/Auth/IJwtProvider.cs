using Domain.Auth;

namespace Application.Common.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateJwtToken(string clientId);
    RefreshToken GenerateRefreshToken(string clientId);
    string? ValidateJwtToken(string token);
}