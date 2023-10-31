using Command.Models;

namespace Command.Commands;

public class RemoveItemCommand : ICommand
{
    private readonly ToDoList _list;

    public RemoveItemCommand(ToDoList list, ToDoItem item)
    {
        _list = list;
        Item = item;
    }

    public ToDoItem Item { get; }

    public void Execute()
    {
        _list.RemoveItem(Item);
    }

    public ICommand GetRevertCommand()
    {
        return new AddItemCommand(_list, Item.Clone());
    }
}