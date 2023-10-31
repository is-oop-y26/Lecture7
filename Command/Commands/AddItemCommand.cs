using Command.Models;

namespace Command.Commands;

public class AddItemCommand : ICommand
{
    private readonly ToDoList _list;

    public AddItemCommand(ToDoList list, ToDoItem item)
    {
        _list = list;
        Item = item;
    }

    public ToDoItem Item { get; }

    public void Execute()
    {
        _list.AddItem(Item);
    }

    public ICommand GetRevertCommand()
    {
        return new RemoveItemCommand(_list, Item);
    }
}