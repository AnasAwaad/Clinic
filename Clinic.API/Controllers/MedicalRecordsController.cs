using Clinic.Application.DTOs.MedicalRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MedicalRecordsController(IMedicalRecordService medicalRecordService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await medicalRecordService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var result = await medicalRecordService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MedicalRecordRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await medicalRecordService.CreateAsync(userId,request);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new {result.Value.Id},result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody]MedicalRecordRequest request)
    {
        var result = await medicalRecordService.UpdateAsync(id,request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await medicalRecordService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
