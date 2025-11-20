using AutoMapper;
using Clinic.Application.DTOs.Auth;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Clinic.Application.Services;
internal class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IJwtProvider jwtProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    IEmailSender emailSender,
    ILogger<AuthService> logger,
    IGoogleAuthService googleAuthService) : IAuthService
{
    private readonly int _refreshTokenExpiryDays = 14;

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request,CancellationToken cancellationToken = default)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(x=>x.Email!.Equals(request.EmailOrUsername) || x.UserName!.Equals(request.EmailOrUsername));

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        if (user.IsDisabled)
            return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

        var result = await signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            // get roles and permissions
            var userRoles = await userManager.GetRolesAsync(user);
            var userPermissions = await unitOfWork.Roles.GetUserPermissionsAsync(userRoles,cancellationToken);
            
            // generate token and refresh token
            (string token, int expiresIn) = jwtProvider.GenerateToken(user, userRoles,userPermissions!);

            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration
            });
            await userManager.UpdateAsync(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token,
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return Result.Success(response);
        }

        if (result.IsLockedOut)
            return Result.Failure<AuthResponse>(UserErrors.LockedUser);

        return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentials);
    }

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
    {
        var user = await userManager.FindByIdAsync(request.Id);

        if (user is null)
            return Result.Failure(UserErrors.InvalidCode);

        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailAlreadyConfirmed);

        string decodedCode;
        try
        {
            decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
        }
        catch (FormatException)
        {
            return Result.Failure(UserErrors.InvalidCode);
        }

        var result = await userManager.ConfirmEmailAsync(user, decodedCode);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        return Result.Success();
    }

    public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Success();

        if (user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailAlreadyConfirmed);

        // Send confirmation email to user
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        logger.LogInformation("ConfirmationCode: {Code}", code);

        await SendConfirmationEmail(user, code);


        return Result.Success();
    }


    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
        var emailExists = await userManager.Users.AnyAsync(x => x.Email == request.Email);
        if (emailExists)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var user = mapper.Map<ApplicationUser>(request);

        var result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, AppRoles.Patient);
            await unitOfWork.Patients.AddAsync(new Patient { Id = user.Id });

            await unitOfWork.SaveAsync();

            // Send confirmation email to user
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


            await SendConfirmationEmail(user, code);

            return Result.Success();
        }

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Token is {token}", token);

        var userId = jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

        if (user.IsDisabled)
            return Result.Failure<AuthResponse>(UserErrors.DisabledUser);


        if (user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthResponse>(UserErrors.LockedUser);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

        if (userRefreshToken is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;


        //var (userRoles, userPermissions) = await GetUserRolesAndPermissionsAsync(user, cancellationToken);
        (string newToken, int expiresIn) = jwtProvider.GenerateToken(user, ["Patient"], null);

        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration,
        });

        await userManager.UpdateAsync(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = newToken,
            ExpiresIn = expiresIn,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return Result.Success(response);
    }

    public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure(UserErrors.InvalidJwtToken);

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure(UserErrors.InvalidJwtToken);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

        if (userRefreshToken is null)
            return Result.Failure(UserErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;

        await userManager.UpdateAsync(user);

        return Result.Success();
    }

    public async Task<Result> SendResetPasswordCodeAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
            return Result.Success();

        if (!user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailNotConfirmed);

        // Send confirmation email to user
        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        logger.LogInformation("Reset code: {Code}", code);

        await SendResetPasswordEmail(user, code);


        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null || !user.EmailConfirmed)
            return Result.Failure(UserErrors.InvalidCode);

        string decodedCode;
        IdentityResult result;
        try
        {
            decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            result = await userManager.ResetPasswordAsync(user, decodedCode, request.NewPassword);
        }
        catch (FormatException)
        {
            result = IdentityResult.Failed(userManager.ErrorDescriber.InvalidToken());
        }

        if (!result.Succeeded)
        {
            logger.LogInformation("Password reset failed for user {UserId}. Errors: {Errors}", user.Id, string.Join(',', result.Errors.Select(e => e.Description)));
            
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        return Result.Success();
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private async Task SendConfirmationEmail(ApplicationUser user, string code)
    {
        var origin = httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
            new Dictionary<string, string>
            {
                { "{{name}}", user.FirstName },
                { "{{action_url}}", $"{origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
            });

        await emailSender.SendEmailAsync(user.Email!, "Confirm your email", emailBody);

    }

    private async Task SendResetPasswordEmail(ApplicationUser user, string code)
    {
        var origin = httpContextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword",
            new Dictionary<string, string>
            {
                { "{{name}}", user.FirstName },
                { "{{action_url}}", $"{origin}/auth/forgetPassword?email={user.Email}&code={code}" }
            });

        await emailSender.SendEmailAsync(user.Email!, "Change password", emailBody);

    }

    public async Task<Result<AuthResponse>> LoginWithGoogleAsync(ExternalAuthDto externalAuth)
    {
        #region validate token and save user
        // validate token
        var payload = await googleAuthService.ValidateIdTokenAsync(externalAuth.IdToken);
        if (payload is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidGoogleToken);

        var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);

        // try find user by external login
        var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        if (user is null)
        {
            // not linked -> try find by email
            user = await userManager.FindByEmailAsync(payload.Email);

            if (user is null)
            {
                // create new user
                user = new ApplicationUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    FirstName = payload.FirstName,
                    LastName = payload.LastName,
                    ImageUrl = payload.Picture
                };

                var createResult = await userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    var error = createResult.Errors.First();
                    return Result.Failure<AuthResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
                }

                var roleResult = await userManager.AddToRoleAsync(user, AppRoles.Patient);
            }

            // add the external login 
            var addLoginResult = await userManager.AddLoginAsync(user, info);
            if (!addLoginResult.Succeeded)
            {
                // possible race or duplicate external login — try to recover by re-querying
                var existing = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (existing is not null)
                {
                    user = existing;
                }
                else
                {
                    var e = addLoginResult.Errors.FirstOrDefault();
                    return Result.Failure<AuthResponse>(new Error(e?.Code ?? "login_error", e?.Description ?? "Failed to add external login", StatusCodes.Status400BadRequest));
                }
            }
        }

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidExternalLogin);

        #endregion

        #region Generate token and refresh token
        var userRoles = await userManager.GetRolesAsync(user);
        var userPermissions = await unitOfWork.Roles.GetUserPermissionsAsync(userRoles);

        // generate token and refresh token
        (string token, int expiresIn) = jwtProvider.GenerateToken(user, userRoles, userPermissions!);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration
        });
        await userManager.UpdateAsync(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = token,
            ExpiresIn = expiresIn,
            RefreshToken = refreshToken,
            RefreshTokenExpiration = refreshTokenExpiration
        };

        #endregion

        return Result.Success<AuthResponse>(response);

    }
}
