using FluentAssertions;
using Shared.Extensions;

namespace Tests.Shared.Extensions;

public class DateTimeOffsetExtensionTest
{
    [Fact]
    public void IsAfter_ExpectTrue_WhenDateTimeOffsetIsAfter()
    {
        DateTimeOffset.UtcNow.IsAfter(DateTimeOffset.UtcNow.AddDays(-1)).Should().BeTrue();
    }

    [Fact]
    public void IsBefore_ExpectTrue_WhenDateTimeOffsetIsBefore()
    {
        DateTimeOffset.UtcNow.IsBefore(DateTimeOffset.UtcNow.AddDays(1)).Should().BeTrue();
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 1)]
    [InlineData(4, 2)]
    [InlineData(5, 2)]
    [InlineData(6, 2)]
    [InlineData(7, 3)]
    [InlineData(8, 3)]
    [InlineData(9, 3)]
    [InlineData(10, 4)]
    [InlineData(11, 4)]
    [InlineData(12, 4)]
    public void GetQuarter_ExpectOne_WhenInFirstQuarter(int month, int expected)
    {
        var dateTimeOffset = new DateTimeOffset(2021, month, 1, 0, 0, 0, TimeSpan.Zero);

        dateTimeOffset.GetQuarter().Should().Be(expected);
    }
}