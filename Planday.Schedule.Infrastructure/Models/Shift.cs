namespace Planday.Schedule.Infrastructure.Models;

public record Shift
{
    public long Id { get; set; }
    public long? EmployeeId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}    