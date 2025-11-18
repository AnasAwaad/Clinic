using Clinic.Application.DTOs.Prescription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController(IPrescriptionService prescriptionService) : ControllerBase
{
    [HttpGet]
    [HasPermission(Permissions.GetPrescriptions)]
    public async Task<IActionResult> GetAll([FromQuery] string? searchValue,[FromQuery]int pageNumber =1, [FromQuery] int pageSize=10)
    {
        var result = await prescriptionService.GetAllAsync(searchValue,pageNumber,pageSize);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.GetPrescriptions)]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var result = await prescriptionService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.AddPrescriptions)]
    public async Task<IActionResult> Create([FromBody] PrescriptionRequest request)
    {
        var result = await prescriptionService.CreateAsync(request);
        if (!result.IsSuccess)
            return result.ToProblem();
        return CreatedAtAction(nameof(GetById), new { id=result.Value.Id }, result.Value);
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdatePrescriptions)]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody] PrescriptionRequest request)
    {
        var result = await prescriptionService.UpdateAsync(id,request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpDelete("{id}")]
    [HasPermission(Permissions.DeletePrescriptions)]
    public async Task<IActionResult> Delete( [FromRoute] int id)
    {
        var result = await prescriptionService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
