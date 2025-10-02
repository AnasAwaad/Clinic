using Clinic.Application.DTOs.Prescription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic.API.Controllers;
[Route("api/medical-records/{recordId}/[controller]")]
[ApiController]
public class PrescriptionsController(IPrescriptionService prescriptionService) : ControllerBase
{
    [HttpGet]
    [HasPermission(Permissions.GetPrescriptions)]
    public async Task<IActionResult> GetAll([FromRoute]int recordId)
    {
        var result = await prescriptionService.GetAllAsync(recordId);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.GetPrescriptions)]
    public async Task<IActionResult> GetById([FromRoute] int recordId,[FromRoute]int id)
    {
        var result = await prescriptionService.GetByIdAsync(recordId,id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.AddPrescriptions)]
    public async Task<IActionResult> Create([FromRoute] int recordId, [FromBody] PrescriptionRequest request)
    {
        var result = await prescriptionService.CreateAsync(User.GetUserId(), recordId, request);
        return CreatedAtAction(nameof(GetById), new { recordId, id=result.Value.Id }, result.Value);
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdatePrescriptions)]
    public async Task<IActionResult> Update([FromRoute] int recordId,[FromRoute]int id, [FromBody] PrescriptionRequest request)
    {
        var result = await prescriptionService.UpdateAsync(recordId,id,request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [HasPermission(Permissions.DeletePrescriptions)]
    public async Task<IActionResult> Delete([FromRoute] int recordId, [FromRoute] int id)
    {
        var result = await prescriptionService.DeleteAsync(recordId,id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
