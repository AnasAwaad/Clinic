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

}
