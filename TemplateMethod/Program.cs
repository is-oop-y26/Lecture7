// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

public record Employee(long Id, string Name);

public record EmployeeRating(
    Employee Employee,
    int TaskCompletedCount,
    double HoursWorked);

public abstract class EmployeeEvaluator
{
    public Employee FindBest(IEnumerable<Employee> employees)
    {
        var ratings = employees.Select(e =>
        {
            var tasks = e.Name.Length / 10;
            var hours = e.Name.Length % 10;

            return new EmployeeRating(e, tasks, hours);
        });

        return Order(ratings).First().Employee;
    }

    protected abstract IOrderedEnumerable<EmployeeRating> Order(
        IEnumerable<EmployeeRating> employeeRatings);
}

public class TasksEmployeeEvaluator : EmployeeEvaluator
{
    protected override IOrderedEnumerable<EmployeeRating> Order(
        IEnumerable<EmployeeRating> employeeRatings)
    {
        return employeeRatings
            .OrderByDescending(x => x.TaskCompletedCount);
    }
}