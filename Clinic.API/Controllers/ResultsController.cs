using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ResultsController(IResultService resultService) : ControllerBase
{

    [HttpGet("patients-per-day")]
    public async Task<IActionResult> PatientsPerDay()
    {
        var result = await resultService.GetPatientsPerDayAsync();
        return Ok(result);
    }

    [HttpGet("appointments-per-day")]
    public async Task<IActionResult> AppointmentsPerDay()
    {
        var result = await resultService.GetAppointmentsPerDayAsync();
        return Ok(result);
    }

    [HttpGet("appointment-status")]
    public async Task<IActionResult> GetStatus([FromQuery] string period = "monthly",
                                               [FromQuery] DateOnly? start = null,
                                               [FromQuery] DateOnly? end = null,
                                               [FromQuery] int? year = null,
                                               CancellationToken ct = default)
    {
        // sanitize period
        period = (period ?? "monthly").Trim().ToLowerInvariant();

        var result = await resultService.GetStatusAsync(period, start, end, year, ct);
        return Ok(result);
    }

}
