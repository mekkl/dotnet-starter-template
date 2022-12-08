using Domain.Common;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Infrastructure.Persistence;

public class RepositoryTest
{
    private readonly Repository<TestEntity> _sut;
    private readonly Mock<DbSet<TestEntity>> _dbSet;

    public RepositoryTest()
    {
        _dbSet = new Mock<DbSet<TestEntity>>();
        var contextMock = new Mock<DbContext>();
        contextMock.Setup(mock => mock.Set<TestEntity>())
            .Returns(_dbSet.Object);
        _sut = new Repository<TestEntity>(contextMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldInvokeAddAsync_WhenCalled()
    {
        // Arrange
        var obj = new TestEntity();

        // Act

        await _sut.AddAsync(obj);

        // Assert
        _dbSet.Verify(mock => mock.AddAsync(obj, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddRangeAsync_ShouldInvokeAddRangeAsync_WhenCalled()
    {
        // Arrange
        var obj = new TestEntity();

        // Act
        await _sut.AddRangeAsync(new[] { obj });

        // Assert
        _dbSet.Verify(mock => mock.AddRangeAsync(It.IsAny<IEnumerable<TestEntity>>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_ShouldInvokeFindAsync_WhenCalled()
    {
        // Arrange
        var id = string.Empty;
        var expected = new TestEntity();

        // Act
        _dbSet.Setup(mock => mock.FindAsync(It.IsAny<string>()))
            .ReturnsAsync(expected);

        var actual = await _sut.GetAsync(id);

        // Assert
        _dbSet.Verify(mock => mock.FindAsync(It.IsAny<string>()), Times.Once);
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task RemoveAsync_ShouldInvokeDelete_WhenCalled()
    {
        // Arrange
        var id = string.Empty;
        var removeEntity = new TestEntity();

        // Act
        _dbSet.Setup(mock => mock.FindAsync(It.IsAny<string>()))
            .ReturnsAsync(removeEntity);

        await _sut.RemoveAsync(id);

        // Assert
        _dbSet.Verify(mock => mock.Remove(removeEntity), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_ShouldNotInvokeDelete_WhenNoEntityExist()
    {
        // Arrange
        var id = string.Empty;

        // Act
        _dbSet.Setup(mock => mock.FindAsync(It.IsAny<string>()))
            .ReturnsAsync((TestEntity?)null);

        await _sut.RemoveAsync(id);

        // Assert
        _dbSet.Verify(mock => mock.Remove(It.IsAny<TestEntity>()), Times.Never);
    }

    public class TestEntity : BaseEntity
    {
    }
}