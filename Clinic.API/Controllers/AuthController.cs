using Clinic.Application.DTOs.Auth;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Entities;
using Clinic.Domain.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService,SignInManager<ApplicationUser> signInManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest dto)
    {
        var result = await authService.RegisterAsync(dto);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        var result = await authService.LoginAsync(dto);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> CreateRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    //// Google Login
    //[HttpPost("login/google")]
    //public IActionResult LoginWithGoogle([FromQuery] string returnUrl = "/")
    //{
    //    var callbackUrl = Url.Action(nameof(LoginWithGoogleCallback), "Auth", new { returnUrl }, Request.Scheme)!;
    //    var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", callbackUrl);
    //    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    //}

    //[HttpGet("login/google/callback")]
    //public async Task<IActionResult> LoginWithGoogleCallback([FromQuery] string returnUrl = "/")
    //{
    //    var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

    //    if (!result.Succeeded)
    //        return Unauthorized();

    //    var loginResult = await authService.LoginWithGoogle(result.Principal);

    //    if (!loginResult.Succeeded)
    //        return BadRequest(new { message = loginResult.Message });

    //    return Ok(loginResult);
    //}
}
