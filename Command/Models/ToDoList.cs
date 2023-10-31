using System.Reactive.Subjects;

namespace Command.Models;

public class ToDoList
{
    private readonly Subject<IEnumerable<ToDoItem>> _subject;
    private readonly List<ToDoItem> _items;

    public ToDoList()
    {
        _subject = new Subject<IEnumerable<ToDoItem>>();
        _items = new List<ToDoItem>();
    }

    public IObservable<IEnumerable<ToDoItem>> Items => _subject;

    public void AddItem(ToDoItem item)
    {
        _items.Add(item);
        _subject.OnNext(_items);
    }

    public void RemoveItem(ToDoItem item)
    {
        _items.Remove(item);
        _subject.OnNext(_items);
    }
}