namespace Application.Common.Interfaces.Messaging;

public class StreamSink<T> : IObservable<T>
{
    private readonly List<IObserver<T>> _observers;

    protected StreamSink(List<IObserver<T>> observers)
    {
        _observers = observers;
    }

    protected async Task Consume(IAsyncEnumerable<T> stream)
    {
        await foreach (var item in stream)
        {
            _observers.ForEach(observer => observer.OnNext(item));
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer)) 
            _observers.Add(observer);
        
        return new UnSubscriber<T>(_observers, observer);
    }
}

internal class UnSubscriber<T> : IDisposable
{
    private readonly List<IObserver<T>> _observers;
    private readonly IObserver<T> _observer;

    internal UnSubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
            _observers.Remove(_observer);
    }
}