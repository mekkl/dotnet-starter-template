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
    private readonly Mock<IMemberRepository> _memberRepositoryMock;

    public DbHealthCheckTest()
    {
        _memberRepositoryMock = new Mock<IMemberRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(mock => mock.MemberRepository)
            .Returns(_memberRepositoryMock.Object);
        _sut = new DbHealthCheck(unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CheckHealthAsync_HasDbConnection_ExpectHealthy()
    {
        _memberRepositoryMock.Setup(mock => mock.ListAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Enumerable.Empty<Member>()));

        var actual = await _sut.CheckHealthAsync(It.IsAny<HealthCheckContext>(), CancellationToken.None);

        actual.Should().NotBeNull();
        actual.Status.Should().Be(HealthStatus.Healthy);
    }
    
    [Fact]
    public async Task CheckHealthAsync_DbConnectionError_ExpectException()
    {
        _memberRepositoryMock.Setup(mock => mock.ListAsync(It.IsAny<CancellationToken>()))
            .Throws<Exception>();

        var act = async () => await _sut.CheckHealthAsync(It.IsAny<HealthCheckContext>(), CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();
    }
}
