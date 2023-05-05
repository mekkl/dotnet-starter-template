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
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Act
        var response = await _httpClient.GetAsync("/health");
        var healthCheck = JsonSerializer.Deserialize<HealthCheckResponse>(await response.Content.ReadAsStringAsync(), jsonSerializerOptions); 

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        healthCheck.Should().NotBeNull();
        healthCheck!.Status.Should().Be(HealthStatus.Healthy.ToString());
        healthCheck.Entries.DbHealth.Status.Should().Be(HealthStatus.Healthy.ToString());
        healthCheck.Entries.Live.Status.Should().Be(HealthStatus.Healthy.ToString());
    }
}

public class Data
{
}

public class DbHealth
{
    public Data Data { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public string Status { get; set; }
    public List<object> Tags { get; set; }
}

public class Entries
{
    public Live Live { get; set; }
    public DbHealth DbHealth { get; set; }
}

public class Live
{
    public Data Data { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public string Status { get; set; }
    public List<object> Tags { get; set; }
}

public class HealthCheckResponse
{
    public string Status { get; set; }
    public string TotalDuration { get; set; }
    public Entries Entries { get; set; }
}

