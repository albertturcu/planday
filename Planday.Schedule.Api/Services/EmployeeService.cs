using Planday.Schedule.Dependencies.EmployeeClient.Client;
using Planday.Schedule.Infrastructure;
using Planday.Schedule.Infrastructure.Models;
using Planday.Schedule.Infrastructure.Repositories;

namespace Planday.Schedule.Api.Services;

public class EmployeeService
{
    private readonly IGenericRepository<Employee> _employeeRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IHttpEmployeeClient _httpEmployeeClient;

    public EmployeeService(IGenericRepository<Employee> employeeRepository, 
        IShiftRepository shiftRepository, HttpEmployeeClient httpEmployeeClient)
    {
        _employeeRepository = employeeRepository;
        _shiftRepository = shiftRepository;
        _httpEmployeeClient = httpEmployeeClient;
    } 
    
    public async Task<bool> EmployeeExistsAsync(int employeeId) => 
        await _employeeRepository.GetAsync(employeeId) != null;

    public async Task<bool> HasOverlappingShiftAsync(int employeeId, DateTime startTime, DateTime endTime) => 
        await _shiftRepository.GetByEmployeeIdAsync(employeeId, startTime, endTime) != null;
    
   public Task<Employee> GetEmployeeByIdAsync(int employeeId) => 
        _httpEmployeeClient.GetEmployee(employeeId);
   
}