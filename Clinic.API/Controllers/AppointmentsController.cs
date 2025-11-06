using Clinic.Application.DTOs.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost]
    [HasPermission(Permissions.AddAppointments)]
    public async Task<IActionResult> Book([FromBody] AppointmentRequest request)
    {
        var result = await appointmentService.CreateAsync(request);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


    [HttpGet("available/{date}")]
    [HasPermission(Permissions.GetTimeSlots)]
    public async Task<IActionResult> GetAvailable([FromRoute] DateOnly date)
    {
        var result = await appointmentService.GetAvailableSlotsAsync(date.DayOfWeek.ToString());
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet]
    [HasPermission(Permissions.GetAppointments)]
    public async Task<IActionResult> GetAll([FromQuery]int pageNumber = 1 , [FromQuery] int pageSize = 10)
    {
        var result = await appointmentService.GetAllAsync(pageNumber,pageSize);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("range")]
    [HasPermission(Permissions.GetAppointments)]
    public async Task<IActionResult> GetRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var result = await appointmentService.GetInRangeAsync(start, end);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.GetAppointments)]
    public async Task<IActionResult> GetDetails([FromRoute] int id)
    {
        var result = await appointmentService.GetAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("my")]
    [HasPermission(Permissions.GetOwnAppointments)]
    public async Task<IActionResult> GetMyAppointments()
    {
        var result = await appointmentService.GetByUserAsync(User.GetUserId());
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id}/cancel")]
    [HasPermission(Permissions.CancelAppointments)]
    public async Task<IActionResult> Cancel(int id)
    {
        var result = await appointmentService.CancelAsync(User.GetUserId(), id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
