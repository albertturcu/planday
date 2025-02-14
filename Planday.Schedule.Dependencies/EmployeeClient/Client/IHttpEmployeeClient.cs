using Planday.Schedule.Infrastructure.Models;

namespace Planday.Schedule.Dependencies.EmployeeClient.Client;

public interface IHttpEmployeeClient
{
    Task<Employee> GetEmployee(int id);
}