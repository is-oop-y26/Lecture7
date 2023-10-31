// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

public record Request(string Value);

public interface IChainLink
{
    void AddNext(IChainLink link);

    void Handle(Request request);
}

public abstract class ChainLinkBase : IChainLink
{
    protected IChainLink? _next;
    
    public void AddNext(IChainLink link)
    {
        if (_next is null)
        {
            _next = link;
        }
        else
        {
            _next.AddNext(link);
        }
    }

    public abstract void Handle(Request request);
}

public class ConsoleHandler : ChainLinkBase
{
    public override void Handle(Request request)
    {
        Console.WriteLine(request.Value);
        _next?.Handle(request);
    }
}

public class FilterHandler : ChainLinkBase
{
    public override void Handle(Request request)
    {
        if (request.Value.Length < 100)
            return;

        _next?.Handle(request);
    }
}