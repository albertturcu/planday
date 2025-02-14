using Microsoft.AspNetCore.Mvc;
using Planday.Schedule.Api.Services;
using Planday.Schedule.Infrastructure.Models;

namespace Planday.Schedule.Api.Controllers;
    
[ApiController]
[Route("[controller]")]
public class ShiftController : ControllerBase
{
    private readonly ShiftService _shiftService; 
    
    public ShiftController(ShiftService shiftService)
    {
        _shiftService = shiftService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        try
        {
            var shift = await _shiftService.GetShiftByIdAsync(id);
            if (shift == null)
                return NotFound(new { Message = "Shift not found" });

            return Ok(shift);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOpenShift([FromBody] Shift shift)
    {
        try
        {
            int shiftId = await _shiftService.CreateOpenShiftAsync(shift);
            return CreatedAtAction(nameof(GetShiftById), new { id = shiftId }, new { Message = "Open shift created", ShiftId = shiftId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    } 
    
    [HttpPut("{shiftId}/assign/{employeeId}")]
    public async Task<IActionResult> AssignShift(int shiftId, int employeeId)
    {
        try
        {
            await _shiftService.AssignShiftAsync(shiftId, employeeId);
            return Ok(new { Message = "Shift assigned successfully." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    } 
}    

