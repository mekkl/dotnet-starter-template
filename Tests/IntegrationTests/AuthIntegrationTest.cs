using System.Net;
using System.Text;
using Application.Auth.Commands;
using FluentAssertions;
using SpecFlow.Internal.Json;

namespace Tests.IntegrationTests;

public class AuthIntegrationTest: IClassFixture<WebAppFactory>
{
    private readonly HttpClient _httpClient;

    public AuthIntegrationTest(WebAppFactory apiFactory)
    {
        _httpClient = apiFactory.CreateClient();
    }

    [Fact, Trait("Category", "IntegrationTest")]
    public async Task Post_Login_ShouldRespondOk()
    {
        // Arrange
        var loginCommand = new LoginCommand
        {
            Email = "someemail@email.com",
            Password = Guid.NewGuid().ToString(),
        };
        var content = new StringContent(loginCommand.ToJson(), Encoding.UTF8, "application/json");
        
        // Act
        var response = await _httpClient.PostAsync("/auth/login", content);
        var token = await response.Content.ReadAsStringAsync(); 

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        token.Should().NotBeNull();
    }
}