namespace Application.Common.Options.Auth;

public class JwtOptions
{
    public const string Jwt = "Jwt";
    
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string Secret { get; init; } = string.Empty;
}