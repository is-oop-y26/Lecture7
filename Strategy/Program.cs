// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var evaluator = new EmployeeEvaluator(
    new DelegateSorter(x => x.OrderByDescending(e => e.TaskCompletedCount)));

public record Employee(long Id, string Name);

public record EmployeeRating(
    Employee Employee,
    int TaskCompletedCount,
    double HoursWorked);

public interface IEmployeeRatingSorter
{
    IOrderedEnumerable<EmployeeRating> Order(
        IEnumerable<EmployeeRating> employeeRatings);
}

public class DelegateSorter : IEmployeeRatingSorter
{
    private readonly Func<IEnumerable<EmployeeRating>, IOrderedEnumerable<EmployeeRating>> _func;

    public DelegateSorter(Func<IEnumerable<EmployeeRating>, IOrderedEnumerable<EmployeeRating>> func)
    {
        _func = func;
    }

    public IOrderedEnumerable<EmployeeRating> Order(IEnumerable<EmployeeRating> employeeRatings)
    {
        return _func.Invoke(employeeRatings);
    }
}

public class EmployeeEvaluator
{
    private readonly IEmployeeRatingSorter _sorter;

    public EmployeeEvaluator(IEmployeeRatingSorter sorter)
    {
        _sorter = sorter;
    }

    public Employee FindBest(IEnumerable<Employee> employees)
    {
        var ratings = employees.Select(e =>
        {
            var tasks = e.Name.Length / 10;
            var hours = e.Name.Length % 10;

            return new EmployeeRating(e, tasks, hours);
        });

        return _sorter.Order(ratings).First().Employee;
    }
}