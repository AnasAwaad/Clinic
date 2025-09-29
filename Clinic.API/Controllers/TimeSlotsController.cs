using Clinic.API.Extensions;
using Clinic.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;

[Route("api")]
[ApiController]
public class TimeSlotsController(IDoctorScheduleService scheduleService) : ControllerBase
{
    
    [HttpPost("days/{day}/timeslots")]
    public async Task<IActionResult> Create([FromRoute] string day, [FromBody] TimeSlotRequest request, CancellationToken cancellationToken)
    {
        var result = await scheduleService.CreateAsync(day, request);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpGet("days/{day}/timeslots")]
    public async Task<IActionResult> GetAll([FromRoute] string day)
    {
        var result = await scheduleService.GetAllAsync(day);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    
    [HttpGet("timeslots/{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var result = await scheduleService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("timeslots/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TimeSlotRequest request)
    {
        var result = await scheduleService.UpdateAsync(id, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("timeslots/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await scheduleService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
