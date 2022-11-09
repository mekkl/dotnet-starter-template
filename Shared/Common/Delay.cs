using System.Runtime.CompilerServices;

namespace Shared.Common;

public readonly struct Delay
{
    private readonly TimeSpan _timeSpan;

    private Delay(TimeSpan timeSpan)
    {
        _timeSpan = timeSpan;
    }
    
    public static Delay Seconds(int seconds)
    {
        return new Delay(TimeSpan.FromSeconds(seconds));
    }
    
    public static Delay Milliseconds(int milliseconds)
    {
        return new Delay(TimeSpan.FromMilliseconds(milliseconds));
    }

    public TaskAwaiter GetAwaiter()
    {
        return Task.Delay(_timeSpan).GetAwaiter();
    }
}