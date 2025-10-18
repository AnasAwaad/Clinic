using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clinic.Application.DTOs.User;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
internal class UserService(UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<UserService> logger,
    IRoleService roleService,
    IFileService fileService) : IUserService
{
    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return await unitOfWork.Users.GetAllUsersWithRoles();
    }

    public async Task<Result<UserResponse>> GetAsync(string id)
    {
        logger.LogInformation("Getting user with id {UserId}", id);
        var user = await userManager.FindByIdAsync(id);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        var userRoles = await userManager.GetRolesAsync(user);

        var resposne = mapper.Map<UserResponse>(user);
        resposne.Roles = userRoles;

        return Result.Success(resposne);
    }

    public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId,CancellationToken cancellationToken = default)
    {
        var result = await userManager.Users
            .Where(x => x.Id == userId)
            .ProjectTo<UserProfileResponse>(mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);

        return Result.Success(result);
    }

    //public async Task<Result<UserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    //{
    //    var emailIsExists = await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

    //    if (emailIsExists)
    //        return Result.Failure<UserResponse>(UserErrors.DuplicatedEmail);

    //    //var allowedRoles = await roleService.GetAllAsync(cancellationToken: cancellationToken);

    //    if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
    //        return Result.Failure<UserResponse>(UserErrors.InvalidRoles);

    //    var user = request.Adapt<ApplicationUser>();
    //    var result = await userManager.CreateAsync(user, request.Password);

    //    if (result.Succeeded)
    //    {
    //        await userManager.AddToRolesAsync(user, request.Roles);

    //        var resposne = (user, request.Roles).Adapt<UserResponse>();

    //        return Result.Success(resposne);
    //    }

    //    var error = result.Errors.First();
    //    return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    //}

    public async Task UpdateProfileAsync(string userId,UpdateProfileRequest request,CancellationToken cancellationToken = default)
    {
        var user = await userManager.Users.SingleAsync(x => x.Id == userId, cancellationToken);

        if (request.ImageProfile is not null)
        {
            if(!string.IsNullOrEmpty(user!.ImageUrl))
                fileService.DeleteFile(user.ImageUrl);

            user.ImageUrl = await fileService.UploadFileAsync(request.ImageProfile, "profiles");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;

        await userManager.UpdateAsync(user!);

        //await userManager.Users
        //    .Where(x => x.Id == userId)
        //    .ExecuteUpdateAsync(setters => setters
        //        .SetProperty(x => x.FirstName, request.FirstName)
        //        .SetProperty(x => x.LastName, request.LastName)
        //        .SetProperty(x => x.PhoneNumber, request.PhoneNumber)
        //    , cancellationToken);

    }

    public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
        var user = await userManager.FindByIdAsync(userId);

        var result = await userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);

        if(result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description,StatusCodes.Status400BadRequest));
    }

    public async Task<Result<UserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var emailIsExists = await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (emailIsExists)
            return Result.Failure<UserResponse>(UserErrors.DuplicatedEmail);

        var allowedRoles = await roleService.GetAllAsync(cancellationToken: cancellationToken);

        if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
            return Result.Failure<UserResponse>(UserErrors.InvalidRoles);

        var user = mapper.Map<ApplicationUser>(request);
        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRolesAsync(user, request.Roles);

            var resposne = mapper.Map<UserResponse>(user);
            resposne.Roles = request.Roles;

            return Result.Success(resposne);
        }

        var error = result.Errors.First();
        return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var emailIsExists = await userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id, cancellationToken);

        if (emailIsExists)
            return Result.Failure<UserResponse>(UserErrors.DuplicatedEmail);

        var userNameIsExists = await userManager.Users.AnyAsync(x => x.UserName == request.UserName && x.Id != id, cancellationToken);

        if (userNameIsExists)
            return Result.Failure<UserResponse>(UserErrors.DuplicatedUsername);

        var allowedRoles = await roleService.GetAllAsync(cancellationToken: cancellationToken);

        if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
            return Result.Failure<UserResponse>(UserErrors.InvalidRoles);

        var user = await userManager.FindByIdAsync(id);
        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        user = mapper.Map(request, user);


        var result = await userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            var roles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, roles);

            await userManager.AddToRolesAsync(user, request.Roles);


            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
    public async Task<Result> ToggleStatus(string userId)
    {
        var user = await userManager.FindByIdAsync(userId)!;

        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        user.IsDisabled = !user.IsDisabled;

        await userManager.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<Result> Unlock(string userId)
    {
        var user = await userManager.FindByIdAsync(userId)!;

        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        var result = await userManager.SetLockoutEndDateAsync(user, null);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }


        return Result.Success();
    }
}
