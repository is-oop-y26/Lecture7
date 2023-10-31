// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var caretaker = new Caretaker(new Originator(""));
caretaker.SetValue("1");
caretaker.SetValue("2");

caretaker.Restore(caretaker.History.First());

Console.WriteLine();

public interface ISnapshot<T>
{
    T Value { get; }
}

public interface IOriginator<T>
{
    ISnapshot<T> CreateSnapshot();

    void Restore(ISnapshot<T> snapshot);
}

public record Snapshot<T>(T Value) : ISnapshot<T>;

public class Originator : IOriginator<Originator>
{
    private string _value;

    public Originator(string value)
    {
        _value = value;
    }

    public void SetValue(string value)
    {
        _value = value;
    }

    public ISnapshot<Originator> CreateSnapshot()
    {
        return new Snapshot<Originator>(Clone());
    }

    public void Restore(ISnapshot<Originator> snapshot)
    {
        _value = snapshot.Value._value;
    }

    public Originator Clone()
    {
        return new Originator(_value);
    }
}

public class Caretaker
{
    private readonly List<ISnapshot<Originator>> _history;
    private readonly Originator _originator;

    public Caretaker(Originator originator)
    {
        _originator = originator;
        _history = new List<ISnapshot<Originator>>();
    }

    public IEnumerable<ISnapshot<Originator>> History => _history;

    public void SetValue(string value)
    {
        _history.Add(_originator.CreateSnapshot());
        _originator.SetValue(value);
    }

    public void Restore(ISnapshot<Originator> snapshot)
    {
        _originator.Restore(snapshot);
    }
}