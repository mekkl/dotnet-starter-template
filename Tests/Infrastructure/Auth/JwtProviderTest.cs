using Application.Common.Options.Auth;
using Domain.Model;
using FluentAssertions;
using Infrastructure.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Infrastructure.Auth;

public class JwtProviderTest
{
    private readonly Mock<IOptions<JwtOptions>> _jwtOptionsMock;
    private readonly JwtProvider _sut;

    public JwtProviderTest()
    {
        var jwtOption = new JwtOptions
        {
            Audience = Guid.NewGuid().ToString(),
            Secret = Guid.NewGuid().ToString(),
            Issuer = Guid.NewGuid().ToString(),
        };
        _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();
        _jwtOptionsMock.Setup(mock => mock.Value)
            .Returns(jwtOption);
        
        _sut = new JwtProvider(_jwtOptionsMock.Object, new Mock<ILogger<JwtProvider>>().Object);
    }

    [Fact]
    public void GenerateJwtToken_GeneratesToken()
    {
        var actual = _sut.GenerateJwtToken(new Member());

        actual.Should().NotBeNull();
    }
    
    [Fact]
    public void GenerateRefreshToken_ShouldReturnTokenWithClientId_WhenClientIdIsGiven()
    {
        var clientId = Guid.NewGuid().ToString();
        _sut.GenerateRefreshToken(clientId).CreatedByClientId.Should().Be(clientId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjbGllbnRJZCIsImV4cCI6MTY3MDUxNzY3OCwiaXNzIjoiODQzMTliMGUtZDA4OC00MTc4LTllYjUtZjYzNWU0ZWE1MjNjIiwiYXVkIjoiOWY4OWU5NDctOWY3ZS00NTU0LTkzZGQtNjA3ZTJjNzIzNWEwIn0.dlVtQLc8OMWsShLMoROq4YyMrd4rmt11xOmaAWZxArI")]
    [InlineData("asd")]
    public void ValidateJwtToken_ShouldReturnNull_WhenTokenInvalid(string token)
    {
        _sut.ValidateJwtToken(token).Should().BeNull();
    }
    
    [Fact]
    public void ValidateJwtToken_ShouldReturnClientId_WhenTokenValid()
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
        };
        var token = _sut.GenerateJwtToken(member);
            
        _sut.ValidateJwtToken(token).Should().Be(member.Id.ToString());
    }
}