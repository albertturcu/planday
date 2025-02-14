using System.Text.Json.Serialization;
using Planday.Schedule.Infrastructure.Models;

namespace Planday.Schedule.Dependencies.EmployeeClient.Models;

public record EmployeeDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}

public static class EmployeeDtoExtensions
{
    public static Employee ToEmployee(this EmployeeDto employeeDto) => 
        new Employee(employeeDto.Name, employeeDto.Email);
}