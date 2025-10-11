using Clinic.Application.DTOs.Auth;
using Clinic.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService,SignInManager<ApplicationUser> signInManager,ILogger<AuthController> logger) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await authService.RegisterAsync(request);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> CreateRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Token is {token}", request.Token);

        var result = await authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        var result = await authService.ConfirmEmailAsync(request);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request)
    {
        var result = await authService.ResendConfirmationEmailAsync(request);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
    {
        await authService.SendResetPasswordCodeAsync(request.Email);

        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await authService.ResetPasswordAsync(request);

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
