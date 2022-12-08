using Domain.Auth;
using FluentAssertions;

namespace Tests.Domain.Auth;

public class RefreshTokenTest
{
    [Fact]
    public void IsExpired_ShouldReturnFalse_IfExpiresInFuture()
    {
        var refreshToken = new RefreshToken
        {
            ExpiresAt = DateTimeOffset.Now.AddDays(1),
        };

        refreshToken.IsExpired.Should().BeFalse();
    }
    
    [Fact]
    public void IsExpired_ShouldReturnTrue_IfExpiresAtInPast()
    {
        var refreshToken = new RefreshToken
        {
            ExpiresAt = DateTimeOffset.Now.AddDays(-1),
        };

        refreshToken.IsExpired.Should().BeTrue();
    }
    
    [Fact]
    public void IsRevoked_ShouldReturnTrue_IfRevokedAtIsSet()
    {
        var refreshToken = new RefreshToken
        {
            RevokedAt = DateTimeOffset.Now.AddDays(-1),
        };

        refreshToken.IsRevoked.Should().BeTrue();
    }
    
    [Fact]
    public void IsRevoked_ShouldReturnFalse_IfRevokedAtNotSet()
    {
        var refreshToken = new RefreshToken();

        refreshToken.IsRevoked.Should().BeFalse();
    }
    
    [Fact]
    public void IsActive_ShouldReturnTrue_IfNotRevokedAndNotExpired()
    {
        var refreshToken = new RefreshToken
        {
            ExpiresAt = DateTimeOffset.Now.AddDays(1),
        };
        
        refreshToken.IsActive.Should().BeTrue();
    }
    
    [Fact]
    public void IsActive_ShouldReturnFalse_IfRevoked()
    {
        var refreshToken = new RefreshToken
        {
            RevokedAt = DateTimeOffset.Now.AddDays(-1),
        };
        
        refreshToken.IsActive.Should().BeFalse();
    }
    
    [Fact]
    public void IsActive_ShouldReturnFalse_IfExpired()
    {
        var refreshToken = new RefreshToken
        {
            ExpiresAt = DateTimeOffset.Now.AddDays(-1),
        };
        
        refreshToken.IsActive.Should().BeFalse();
    }
    
    [Fact]
    public void IsActive_ShouldReturnFalse_IfExpiredAndRevoked()
    {
        var refreshToken = new RefreshToken
        {
            RevokedAt = DateTimeOffset.Now.AddDays(-1),
            ExpiresAt = DateTimeOffset.Now.AddDays(-1),
        };
        
        refreshToken.IsActive.Should().BeFalse();
    }
}