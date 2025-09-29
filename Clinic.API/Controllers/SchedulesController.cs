using Clinic.API.Extensions;
using Clinic.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SchedulesController(IDoctorScheduleService scheduleService) : ControllerBase
{

    [HttpPost("day/{day}/slot")]
    public async Task<IActionResult> Create([FromRoute] string day, [FromBody]DoctorScheduleRequest request , CancellationToken cancellationToken)
    {
        var doctorId = 1;
        var result = await scheduleService.CreateAsync(doctorId,day, request);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
