using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace Tests.IntegrationTests;

public class AdminIntegrationTest : IClassFixture<WebAppFactory>
{
    private readonly HttpClient _httpClient;

    public AdminIntegrationTest(WebAppFactory apiFactory)
    {
        _httpClient = apiFactory.CreateClient();
    }

    [Fact, Trait("Category", "IntegrationTest")]
    public async Task Get_Health_ShouldRespondHealthy()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/admin/servertime");
        var healthCheck = JsonSerializer.Deserialize<DateTimeOffset>(await response.Content.ReadAsStringAsync()); 

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        healthCheck.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromSeconds(10));
    }
}