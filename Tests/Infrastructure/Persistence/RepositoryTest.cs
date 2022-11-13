using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Infrastructure.Persistence;

public class RepositoryTest
{
    private readonly Repository<object> _sut;
    private readonly Mock<DbSet<object>> _dbSet;

    public RepositoryTest()
    {
        _dbSet = new Mock<DbSet<object>>();
        var contextMock = new Mock<DbContext>();
        contextMock.Setup(mock => mock.Set<object>())
            .Returns(_dbSet.Object);
        _sut = new Repository<object>(contextMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldInvokeAddAsync_WhenCalled()
    {
        // Arrange
        var obj = new object();

        // Act

        await _sut.AddAsync(obj);

        // Assert
        _dbSet.Verify(mock => mock.AddAsync(obj, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddRangeAsync_ShouldInvokeAddRangeAsync_WhenCalled()
    {
        // Arrange
        var obj = new object();

        // Act
        await _sut.AddRangeAsync(new[] { obj });

        // Assert
        _dbSet.Verify(mock => mock.AddRangeAsync(It.IsAny<IEnumerable<object>>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAsync_ShouldInvokeFindAsync_WhenCalled()
    {
        // Arrange
        var id = string.Empty;
        var expected = new object();

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
        object removeEntity = new();

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
            .ReturnsAsync(null);

        await _sut.RemoveAsync(id);

        // Assert
        _dbSet.Verify(mock => mock.Remove(It.IsAny<object>()), Times.Never);
    }
}