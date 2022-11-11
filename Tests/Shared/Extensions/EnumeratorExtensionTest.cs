using FluentAssertions;
using Shared.Extensions;

namespace Tests.Shared.Extensions;

public class EnumeratorExtensionTest
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(200, 201)]
    [InlineData(3, 4)]
    [InlineData(0, 1)]
    [InlineData(5125, 5126)]
    [InlineData(null, 1)]
    public void CustomIntEnumerator_IntValue_ExpectTimes(int size, int expected)
    {
        var timesCalled = 0;

        foreach (var _ in size)
        {
            ++timesCalled;
        }

        timesCalled.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(1, 2, 2)]
    [InlineData(200, 250, 51)]
    [InlineData(3, 9, 7)]
    [InlineData(0, 1, 2)]
    [InlineData(5125, 5125, 1)]
    [InlineData(2, 1, 0)]
    [InlineData(null, 1, 2)]
    public void CustomIntEnumerator_RangeValue_ExpectTimes(int start, int end, int expected)
    {
        var timesCalled = 0;

        foreach (var _ in start..end)
        {
            ++timesCalled;
        }

        timesCalled.Should().Be(expected);
    }
}