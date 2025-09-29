using Clinic.Application.DTOs.Appointment;
using Clinic.Infrastructure.Data.Seeds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Book([FromBody] AppointmentRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await appointmentService.CreateAsync(userId,request);
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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await appointmentService.GetByUserAsync(userId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id}/cancel")]
    [Authorize]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await appointmentService.CancelAsync(userId,id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
