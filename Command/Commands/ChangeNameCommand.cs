using Command.Models;

namespace Command.Commands;

public class ChangeNameCommand : ICommand
{
    public ChangeNameCommand(ToDoItem item, string name)
    {
        Item = item;
        Name = name;
    }

    public ToDoItem Item { get; }

    public ToDoItem? OldItem { get; private set; }

    public string Name { get; }

    public void Execute()
    {
        OldItem = Item.Clone();
        Item.Name = Name;
    }

    public ICommand GetRevertCommand()
    {
        if (OldItem is null)
            return new NullCommand();

        return new ChangeNameCommand(Item, OldItem.Name);
    }
}