using Clinic.Application.DTOs.Auth;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Consts;
using Clinic.Domain.Entities;
using Clinic.Domain.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
internal class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ITokenService tokenService,
    IUnitOfWork unitOfWork) : IAuthService
{
    public async Task<Result<LoginResult>> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return Result<LoginResult>.Fail("Invalid email or password.");



        var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            return Result<LoginResult>.Fail("Invalid email or password.");


        var role = await userManager.GetRolesAsync(user);
        var token = tokenService.GenerateToken(user, role.First());

        var loginResult = new LoginResult
        {
            Token = token,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        return Result<LoginResult>.Success(loginResult, "Logged in successfully.");
    }

    public async Task<Result<LoginResult>> LoginWithGoogle(ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal is null)
            return Result<LoginResult>.Fail("Claim principal is null");

        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

        if(email is null)
            return Result<LoginResult>.Fail("Email is null");

        var user = await userManager.FindByEmailAsync(email);

        if(user is null)
        {
            var newUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newUser);

            if (!result.Succeeded)
                return Result<LoginResult>.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

            var roleResult = await userManager.AddToRoleAsync(newUser, AppRoles.Patient);
            if (!roleResult.Succeeded)
                return Result<LoginResult>.Fail("Failed to assign role");

            user = newUser;
        }

        var info = new UserLoginInfo("Google",
            claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            "Google");

        var existingLogins = await userManager.GetLoginsAsync(user);
        var alreadyLinked = existingLogins.Any(l => l.LoginProvider == "Google" && l.ProviderKey == email);

        if (!alreadyLinked)
        {
            var loginResult = await userManager.AddLoginAsync(user, info);
            if (!loginResult.Succeeded)
                return Result<LoginResult>.Fail(string.Join(", ", loginResult.Errors.Select(e => e.Description)));
        }


        var role = await userManager.GetRolesAsync(user);
        var token = tokenService.GenerateToken(user, role.First());

        var resultResponse = new LoginResult
        {
            Token = token,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        return Result<LoginResult>.Success(resultResponse, "Logged in successfully.");
    }

    public async Task<Result<string>> RegisterAsync(RegisterPatientDto dto)
    {
        var testUser = await userManager.FindByEmailAsync(dto.Email);
        if (testUser != null)
            return Result<string>.Fail("Email Already Exists");

        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return Result<string>.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));


        var roleResult = await userManager.AddToRoleAsync(user, AppRoles.Patient);
        if (!roleResult.Succeeded)
            return Result<string>.Fail("Failed to assign role"); 

        await unitOfWork.Patients.AddAsync(new Patient { UserId = user.Id});
        await unitOfWork.SaveAsync();


        return Result<string>.Success("","User Registered Successfully");

    }

}
