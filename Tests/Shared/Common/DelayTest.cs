using System.Diagnostics;
using FluentAssertions;
using Shared.Common;

namespace Tests.Shared.Common;

public class DelayTest
{
    private const int MsMargin = 10;

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    public async Task Delay_Seconds_ExpectWithinRange(int seconds)
    {
        var sw = Stopwatch.StartNew();
        await Delay.Seconds(seconds);
        sw.Stop();
        
        sw.Elapsed.Milliseconds.Should().BeGreaterOrEqualTo((seconds * 1000) - MsMargin);
    }
    
    [Theory]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(1100)]
    public async Task Delay_Milliseconds_ExpectWithinRange(int milliseconds)
    {
        var sw = Stopwatch.StartNew();
        await Delay.Milliseconds(milliseconds);
        sw.Stop();
        
        sw.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(milliseconds - MsMargin);
    }
}