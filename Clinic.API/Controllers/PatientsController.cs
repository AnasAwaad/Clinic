using Clinic.Application.DTOs.Patient;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PatientsController(IPatientService patientService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber=1, [FromQuery] int pageSize=10)
    {
        var result = await patientService.GetAll(pageNumber, pageSize);
        return Ok(result.Value);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActivePatients()
    {
        var result = await patientService.GetAllActivePatients();
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]PatientRequest request)
    {
        var result = await patientService.CreateAsync(request);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]string id)
    {
        var result = await patientService.GetById(id);
        return Ok(result.Value);
    }
}

