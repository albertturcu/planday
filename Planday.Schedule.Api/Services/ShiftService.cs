using Planday.Schedule.Api.Services.Models;
using Planday.Schedule.Infrastructure.Models;
using Planday.Schedule.Infrastructure.Repositories;

namespace Planday.Schedule.Api.Services;

public class ShiftService
{
    private readonly IShiftRepository _shiftRepository;
    private readonly EmployeeService _employeeService;

    public ShiftService(
        IShiftRepository shiftRepository,
        EmployeeService employeeService
        )
    {
        _shiftRepository = shiftRepository;
        _employeeService = employeeService;
    }

    public async Task<DetailedShift?> GetShiftByIdAsync(int shiftId)
    {
        var shift = await _shiftRepository.GetAsync(shiftId);

        if (shift == null)
            return null;
        
        if (shift.EmployeeId == null)
            return new DetailedShift(shift.Id, shift.EmployeeId, "", shift.Start, shift.End);
        
        var employee = await _employeeService.GetEmployeeByIdAsync((int)shift.EmployeeId);
       
        return new DetailedShift(shift.Id, shift.EmployeeId, employee.Email, shift.Start, shift.End);
    }

    public async Task<int> CreateOpenShiftAsync(Shift shift)
    {
        if (shift.EmployeeId != null)
            throw new ArgumentException("An open shift must not have an employee.");
        
        if (shift.Start.Date != shift.End.Date)
            throw new ArgumentException("Shift must start and end on the same day.");
        
        if (shift.Start >= shift.End)
            throw new ArgumentException("Start time must be before end time.");
        

        return await _shiftRepository.AddAsync(shift);
    }

    public async Task AssignShiftAsync(int shiftId, int employeeId)
    {
       var shift = await _shiftRepository.GetAsync(shiftId);
        
        if (shift == null)
            throw new ArgumentException("Shift does not exist.");

        if (!await _employeeService.EmployeeExistsAsync(employeeId))
            throw new ArgumentException("Employee does not exist.");

        if (shift.EmployeeId != null)
            throw new ArgumentException("This shift is already assigned to an employee.");

        if (await _employeeService.HasOverlappingShiftAsync(employeeId, shift.Start, shift.End))
            throw new ArgumentException("Employee has an overlapping shift.");

        shift.EmployeeId = employeeId;
        await _shiftRepository.UpdateAsync(shift);
    } 
}