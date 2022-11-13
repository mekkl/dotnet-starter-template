using Application.Common.Interfaces.Persistence;
using Domain.Model;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MinimalApi.Health;
using Moq;

namespace Tests.MinimalApi.Health;

public class DbHealthCheckTest
{
    private readonly DbHealthCheck _sut;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public DbHealthCheckTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _sut = new DbHealthCheck(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task CheckHealthAsync_HasDbConnection_ExpectHealthy()
    {
        _userRepositoryMock.Setup(mock => mock.ListAsync())
            .Returns(Task.FromResult(Enumerable.Empty<User>()));

        var actual = await _sut.CheckHealthAsync(It.IsAny<HealthCheckContext>(), CancellationToken.None);

        actual.Should().NotBeNull();
        actual.Status.Should().Be(HealthStatus.Healthy);
    }
    
    [Fact]
    public async Task CheckHealthAsync_DbConnectionError_ExpectException()
    {
        _userRepositoryMock.Setup(mock => mock.ListAsync())
            .Throws<Exception>();

        var act = async () => await _sut.CheckHealthAsync(It.IsAny<HealthCheckContext>(), CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }
}
