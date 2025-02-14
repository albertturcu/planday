namespace Planday.Schedule.Api.Services.Models;

public class DetailedShift
{
    public long Id { get; set; }
    public long? EmployeeId { get; set; }
    public string? EmployeeEmail { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    
    public DetailedShift(
        long id, long? employeeId, string employeeEmail, DateTime start, DateTime end)
    {
        Id= id;
        EmployeeId = employeeId;
        EmployeeEmail  = employeeEmail;
        Start = start;
        End = end;
    }
}    