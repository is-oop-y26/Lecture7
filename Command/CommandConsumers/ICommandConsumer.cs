using Command.Commands;

namespace Command.CommandConsumers;

public interface ICommandConsumer
{
    void Consume(ICommand command);
}