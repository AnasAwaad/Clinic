using Clinic.Application.DTOs.MedicalRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MedicalRecordsController(IMedicalRecordService medicalRecordService) : ControllerBase
{
    [HttpGet]
    [HasPermission(Permissions.GetMedicalRecords)]
    public async Task<IActionResult> GetAll()
    {
        var result = await medicalRecordService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.GetMedicalRecords)]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var result = await medicalRecordService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.AddMedicalRecords)]
    public async Task<IActionResult> Create([FromBody] MedicalRecordRequest request)
    {
        var result = await medicalRecordService.CreateAsync(User.GetUserId(), request);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new {result.Value.Id},result.Value) : result.ToProblem();
    }

    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateMedicalRecords)]
    public async Task<IActionResult> Update([FromRoute]int id, [FromBody]MedicalRecordRequest request)
    {
        var result = await medicalRecordService.UpdateAsync(id,request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }


    [HttpDelete("{id}")]
    [HasPermission(Permissions.DeleteMedicalRecords)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await medicalRecordService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
