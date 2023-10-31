namespace Command.Models;

public class ToDoItem
{
    public ToDoItem(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public ToDoItem Clone()
    {
        return new ToDoItem(Name);
    }
}