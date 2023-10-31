using System.Reactive.Subjects;
using Command.Commands;

namespace Command.CommandConsumers;

public class CommandHistory
{
    private readonly SubjectBase<IEnumerable<ICommand>> _subject;
    private readonly List<ICommand> _commands;

    public CommandHistory()
    {
        _subject = new ReplaySubject<IEnumerable<ICommand>>(1);
        _commands = new List<ICommand>();
    }

    public IObservable<IEnumerable<ICommand>> Commands => _subject;

    public void AddCommand(ICommand command)
    {
        _commands.Insert(0, command);
        _subject.OnNext(_commands);
    }
}