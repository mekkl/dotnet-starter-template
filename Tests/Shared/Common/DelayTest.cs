using System.Diagnostics;
using FluentAssertions;
using Shared.Common;

namespace Tests.Shared.Common;

public class DelayTest
{
    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    public async Task Delay_Seconds_ExpectWithinRange(int seconds)
    {
        var sw = Stopwatch.StartNew();
        await Delay.Seconds(seconds);
        sw.Stop();
        
        sw.Elapsed.Seconds.Should().BeGreaterOrEqualTo(seconds);
    }
    
    [Theory]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(1100)]
    public async Task Delay_Milliseconds_ExpectWithinRange(int milliseconds)
    {
        const int msMargin = 10;
        var sw = Stopwatch.StartNew();
        await Delay.Milliseconds(milliseconds);
        sw.Stop();
        
        sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(milliseconds - msMargin);
    }
}