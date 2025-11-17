using Clinic.Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/me")]
[ApiController]
[Authorize]
public class AccountController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Info()
    {
        var result = await userService.GetProfileAsync(User.GetUserId());
        
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("info")]
    public async Task<IActionResult> Info([FromForm] UpdateProfileRequest request)
    {
        await userService.UpdateProfileAsync(User.GetUserId(), request);

        return NoContent();
    }


    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await userService.ChangePasswordAsync(User.GetUserId(), request);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
