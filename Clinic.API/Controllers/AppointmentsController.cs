using Clinic.Application.DTOs.Appointment;
using Clinic.Application.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpPost]
    [HasPermission(Permissions.AddAppointments)]
    public async Task<IActionResult> Create([FromBody] AppointmentRequest request)
    {
        var result = await appointmentService.CreateAsync(request);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("book")]
    public async Task<IActionResult> Book([FromBody] BookAppointmentRequest request)
    {
        var result = await appointmentService.BookAsync(User.GetUserId(),request);
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
    public async Task<IActionResult> GetAll([FromQuery] RequestFilters request)
    {
        var result = await appointmentService.GetAllAsync(request);
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
    //[HasPermission(Permissions.GetAppointments)]
    public async Task<IActionResult> GetDetails([FromRoute] int id)
    {
        var result = await appointmentService.GetAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("my")]
    [HasPermission(Permissions.GetOwnAppointments)]
    public async Task<IActionResult> GetMyAppointments([FromQuery] RequestFilters request, [FromQuery] string type = "Today")
    {
        var result = await appointmentService.GetMyAppointmentsAsync(User.GetUserId(),request,type);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{id}/cancel")]
    //[HasPermission(Permissions.UpdateAppointments)]
    public async Task<IActionResult> Cancel(int id)
    {
        var result = await appointmentService.CancelAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/complete")]
    [HasPermission(Permissions.UpdateAppointments)]
    public async Task<IActionResult> Complete(int id)
    {
        var result = await appointmentService.CompleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}")]
    //[HasPermission(Permissions.UpdateAppointments)]
    public async Task<IActionResult> Update([FromRoute]int id ,[FromBody] AppointmentRequest request)
    {
        var result = await appointmentService.UpdateAsync(id, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBulk([FromBody] BulkDeleteRequest<int> request)
    {
        var result = await appointmentService.DeleteManyAsync(request.Ids);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var result = await appointmentService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
