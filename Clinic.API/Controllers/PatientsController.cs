using Clinic.Application.DTOs.Appointment;
using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Patient;
using Clinic.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PatientsController(IPatientService patientService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RequestFilters request)
    {
        var result = await patientService.GetAllAsync(request);
        return Ok(result.Value);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActivePatients()
    {
        var result = await patientService.GetAllActivePatientsAsync();
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]PatientRequest request)
    {
        var result = await patientService.CreateAsync(request);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]string id,[FromForm] UpdatePatientRequest request)
    {
        var result = await patientService.UpdateAsync(id,request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]string id)
    {
        var result = await patientService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var result = await patientService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete]
    public IActionResult DeleteBulk([FromBody] BulkDeleteRequest<string> request)
    {
        var result = patientService.DeleteMany(request.Ids);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpGet("{patientId}/details")]
    public async Task<ActionResult<PatientProfileResponse>> GetPatientProfileDetails(string patientId)
    {
        var result = await patientService.GetPatientProfileDetailsAsync(patientId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}

