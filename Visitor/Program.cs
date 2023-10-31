// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var component = new AggregateComponent(new IComponent[]
{
    new AggregateComponent(new[]
    {
        new NameComponent("aboba"),
    }),
    new NameComponent("upper aboba"),
});

var visitor = new Visitor();

component.Accept(visitor);

public interface IComponent
{
    void Accept(IVisitor visitor);
}

public interface IVisitor { }

public interface IVisitor<T> : IVisitor where T : IComponent
{
    void Visit(T component);
}

public class NameComponent : IComponent
{
    public NameComponent(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void Accept(IVisitor visitor)
    {
        if (visitor is IVisitor<NameComponent> v)
            v.Visit(this);
    }
}

public class AggregateComponent : IComponent
{
    public AggregateComponent(IEnumerable<IComponent> components)
    {
        Components = components;
    }

    public IEnumerable<IComponent> Components { get; }

    public void Accept(IVisitor visitor)
    {
        if (visitor is IVisitor<AggregateComponent> v)
            v.Visit(this);
    }
}

public class Visitor : IVisitor<AggregateComponent>, IVisitor<NameComponent>
{
    private int _depth;

    public void Visit(AggregateComponent component)
    {
        var s = string.Concat(Enumerable.Repeat('\t', _depth));
        Console.WriteLine(s + "aggregate");

        _depth += 1;

        foreach (var c in component.Components)
        {
            c.Accept(this);
        }

        _depth -= 1;
    }

    public void Visit(NameComponent component)
    {
        var s = string.Concat(Enumerable.Repeat('\t', _depth));
        Console.WriteLine(s + component.Name);
    }
}