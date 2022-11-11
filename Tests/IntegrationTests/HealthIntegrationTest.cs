using System.Net;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MinimalApi.Health;

namespace Tests.IntegrationTests;

public class HealthIntegrationTest : IClassFixture<WebAppFactory>
{
    private readonly HttpClient _httpClient;

    public HealthIntegrationTest(WebAppFactory apiFactory)
    {
        _httpClient = apiFactory.CreateClient();
    }

    [Fact, Trait("Category", "IntegrationTest")]
    public async Task Get_Health_ShouldRespondHealthy()
    {
        // Arrange

        // Act
        var response = await _httpClient.GetAsync("/health");
        var healthCheck = JsonSerializer.Deserialize<HealthCheckResponse>(await response.Content.ReadAsStringAsync()); 

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        healthCheck.Should().NotBeNull();
        healthCheck!.HealthStatus.Should().Be(HealthStatus.Healthy);
        healthCheck.HealthChecks.Should().AllSatisfy(check => check.HealthStatus.Should().Be(HealthStatus.Healthy));
    }
}