using Domain.Model;

namespace Application.Common.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateJwtToken(Member member);
    RefreshToken GenerateRefreshToken(string clientId);
    string? ValidateJwtToken(string token);
}