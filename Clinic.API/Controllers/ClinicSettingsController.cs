using Clinic.Application.DTOs.Clinic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/clinic-settings")]
[ApiController]
public class ClinicSettingsController(IClinicSettingsService clinicSettingsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ClinicSettingsResponse>> Get()
    {
        return Ok(await clinicSettingsService.GetAsync());
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromForm] ClinicSettingsRequest request)
    {
        await clinicSettingsService.UpdateAsync(request);
        return NoContent();
    }
}
