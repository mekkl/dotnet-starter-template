using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces.Auth;
using Application.Common.Options.Auth;
using Domain.Constants;
using Domain.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<JwtProvider> _logger;

    public JwtProvider(IOptions<JwtOptions> jwtOptions, ILogger<JwtProvider> logger)
    {
        _logger = logger;
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateJwtToken(Member member)
    {
        var permissions = member.Roles.SelectMany(role => role.Permissions).ToHashSet();
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
        };
        
        claims.AddRange(permissions
            .Select(permission => new Claim(CustomClaims.Permissions, permission.ToString()!)));

        var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.Now.AddHours(1),
            signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public RefreshToken GenerateRefreshToken(string clientId)
    {
        // generate token that is valid for 7 days
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(7),
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedByClientId = clientId,
        };

        return refreshToken;
    }

    public string? ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
        SecurityToken? validatedToken;

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
            }, out validatedToken);
        }
        catch (ArgumentException e)
        {
            _logger.LogDebug(e, "Could not validate token!");
            return null;
        }
        catch (SecurityTokenSignatureKeyNotFoundException e)
        {
            _logger.LogDebug(e, "Could not validate token!");
            return null;
        }

        var jwtToken = (JwtSecurityToken?)validatedToken;
        var clientId = jwtToken?.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        
        return clientId;
    }
}