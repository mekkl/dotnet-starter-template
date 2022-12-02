using System.Runtime.CompilerServices;

namespace Shared.Common;

public readonly struct Delay
{
    private readonly TimeSpan _timeSpan;
    private readonly CancellationToken _cancellationToken;

    private Delay(TimeSpan timeSpan, CancellationToken cancellationToken = default)
    {
        _timeSpan = timeSpan;
        _cancellationToken = cancellationToken;
    }
    
    public static Delay Seconds(int seconds, CancellationToken cancellationToken = default)
    {
        return new Delay(TimeSpan.FromSeconds(seconds), cancellationToken);
    }
    
    public static Delay Milliseconds(int milliseconds, CancellationToken cancellationToken = default)
    {
        return new Delay(TimeSpan.FromMilliseconds(milliseconds), cancellationToken);
    }

    public TaskAwaiter GetAwaiter()
    {
        return Task.Delay(_timeSpan, _cancellationToken).GetAwaiter();
    }
}