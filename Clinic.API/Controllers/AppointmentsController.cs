using Clinic.Application.DTOs.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Book([FromBody] AppointmentRequest request)
    {
        var result = await appointmentService.CreateAsync(User.GetUserId(), request);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("available/{date}")]
    public async Task<IActionResult> GetAvailable([FromRoute] DateOnly date)
    {
        var result = await appointmentService.GetAvailableSlotsAsync(date.DayOfWeek.ToString());
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetDetails([FromRoute] int id)
    {
        var result = await appointmentService.GetAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMyAppointments()
    {
        var result = await appointmentService.GetByUserAsync(User.GetUserId());
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id}/cancel")]
    [Authorize]
    public async Task<IActionResult> Cancel(int id)
    {
        var result = await appointmentService.CancelAsync(User.GetUserId(), id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
