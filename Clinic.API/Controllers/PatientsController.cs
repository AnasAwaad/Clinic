using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PatientsController(IPatientService patientService) : ControllerBase
{
    [HttpGet("active")]
    public async Task<IActionResult> GetAllActivePatients()
    {
        var result = await patientService.GetAllActivePatients();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
