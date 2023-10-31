// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var s = new Submission();

s.Complete();
s.Ban();

public class Submission
{
    private ISubmissionState _state;

    public Submission()
    {
        _state = new ActiveState();
    }

    public void Activate()
    {
        _state = _state.MoveToActive();
    }

    public void Complete()
    {
        _state = _state.MoveToCompleted();
    }

    public void Ban()
    {
        _state = _state.MoveToBanned();
    }
}

public interface ISubmissionState
{
    ISubmissionState MoveToActive();

    ISubmissionState MoveToCompleted();

    ISubmissionState MoveToBanned();
}

public class ActiveState : ISubmissionState
{
    public ISubmissionState MoveToActive()
    {
        return this;
    }

    public ISubmissionState MoveToCompleted()
    {
        return new CompletedState();
    }

    public ISubmissionState MoveToBanned()
    {
        return new BannedState();
    }
}

public class CompletedState : ISubmissionState
{
    public ISubmissionState MoveToActive()
    {
        throw new InvalidOperationException();
    }

    public ISubmissionState MoveToCompleted()
    {
        return this;
    }

    public ISubmissionState MoveToBanned()
    {
        return new BannedState();
    }
}

public class BannedState : ISubmissionState
{
    public ISubmissionState MoveToActive()
    {
        throw new InvalidOperationException();
    }

    public ISubmissionState MoveToCompleted()
    {
        throw new InvalidOperationException();
    }

    public ISubmissionState MoveToBanned()
    {
        return this;
    }
}