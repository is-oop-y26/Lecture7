using Command.Commands;

namespace Command.CommandConsumers;

public class CommandConsumer : ICommandConsumer
{
    private readonly CommandHistory _history;

    public CommandConsumer(CommandHistory history)
    {
        _history = history;
    }

    public void Consume(ICommand command)
    {
        command.Execute();
        _history.AddCommand(command);
    }
}