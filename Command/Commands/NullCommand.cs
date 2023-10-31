namespace Command.Commands;

public class NullCommand : ICommand
{
    public void Execute() { }

    public ICommand GetRevertCommand()
        => this;
}