using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Infrastructure.Persistence;

public class AppDbConnectionFactoryTest
{
    private readonly string _connectionString;
    private readonly AppDbConnectionFactory _sut;

    public AppDbConnectionFactoryTest()
    {
        _connectionString = "Data Source=db,1433; Persist Security Info=True;User ID=SA;Password=yourStrong(!)Password; TrustServerCertificate=True";
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(mock => mock[It.IsAny<string>()])
            .Returns(_connectionString);
        
        _sut = new AppDbConnectionFactory(configurationMock.Object);
    }
    
    [Fact]
    public void CheckHealthAsync_HasDbConnection_ExpectHealthy()
    {
        var actual = _sut.CreateConnection();

        actual.Should().NotBeNull();
        actual.ConnectionString.Should().Be(_connectionString);
    }
}
