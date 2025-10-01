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
    ILogger<UserService> logger) : IUserService
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

    public async Task UpdateProfileAsync(string userId,UpdateProfileRequest request,CancellationToken cancellationToken = default)
    {
        await userManager.Users
            .Where(x => x.Id == userId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.FirstName, request.FirstName)
                .SetProperty(x => x.LastName, request.LastName)
                .SetProperty(x => x.PhoneNumber, request.PhoneNumber)
            ,cancellationToken);

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


}
