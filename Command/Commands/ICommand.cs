namespace Command.Commands;

public interface ICommand
{
    void Execute();

    ICommand GetRevertCommand();
}