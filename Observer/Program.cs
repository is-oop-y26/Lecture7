// See https://aka.ms/new-console-template for more information

Console.WriteLine();

// var subject = new Subject<string>();
//
// subject.Subscribe(Console.WriteLine);
//
// subject.OnNext("1");
// subject.OnNext("2");

var publisher = new Publisher<string>();
publisher.Subscribe(new Observer<string>(Console.WriteLine));

publisher.SendNext("1");
publisher.SendNext("2");

public interface IObserver<in T>
{
    void OnNext(T value);
}

public interface IPublisher<out T>
{
    void Subscribe(IObserver<T> observer);
}

public class Publisher<T> : IPublisher<T>
{
    private readonly List<IObserver<T>> _observers;

    public Publisher()
    {
        _observers = new List<IObserver<T>>();
    }

    public void Subscribe(IObserver<T> observer)
    {
        _observers.Add(observer);
    }

    public void SendNext(T value)
    {
        foreach (IObserver<T> observer in _observers)
        {
            observer.OnNext(value);
        }
    }
}

public class Observer<T> : IObserver<T>
{
    private readonly Action<T> _action;

    public Observer(Action<T> action)
    {
        _action = action;
    }

    public void OnNext(T value)
    {
        _action.Invoke(value);
    }
}