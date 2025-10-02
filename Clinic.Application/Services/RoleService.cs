using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clinic.Application.DTOs.Role;
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
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
internal class RoleService(RoleManager<ApplicationRole> roleManager,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<RoleService> logger) : IRoleService
{
    public async Task<Result<RoleDetailResponse>> CreateAsync(RoleRequest request)
    {
        var roleIsExists = await roleManager.RoleExistsAsync(request.Name);

        if (roleIsExists)
            return Result.Failure<RoleDetailResponse>(RoleErrors.DuplicatedRoleTitle);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermission);

        var role = new ApplicationRole
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var result = await roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            var permissions = request.Permissions.Select(x => new IdentityRoleClaim<string>
            {
                RoleId = role.Id,
                ClaimType = Permissions.Type,
                ClaimValue = x
            });

            await unitOfWork.RoleClaims.AddRangeAsync(permissions);
            await unitOfWork.SaveAsync();

            var response = new RoleDetailResponse
            {
                Id = role.Id,
                Name = role.Name!,
                IsDeleted = role.IsDeleted,
                Permissions = request.Permissions
            };

            return Result.Success(response);
        }

        var error = result.Errors.First();

        return Result.Failure<RoleDetailResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<IEnumerable<RoleResponse>> GetAllAsync(bool? includeDisabled = false, CancellationToken cancellationToken = default) =>
        await roleManager.Roles
        .Where(r => !r.IsDefault && (!r.IsDeleted || (includeDisabled.HasValue && includeDisabled.Value)))
        .ProjectTo<RoleResponse>(mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

    public async Task<Result<RoleDetailResponse>> GetAsync(string id)
    {
        var role = await roleManager.FindByIdAsync(id);

        logger.LogInformation("Role: {@id}", id);

        if (role is null)
            return Result.Failure<RoleDetailResponse>(RoleErrors.RoleNotFound);

        var permissions = await roleManager.GetClaimsAsync(role);

        var response = new RoleDetailResponse
        {
            Id = role.Id,
            Name = role.Name!,
            IsDeleted = role.IsDeleted,
            Permissions = permissions.Select(r => r.Value)
        };

        return Result.Success(response);
    }

    public async Task<Result> ToggleStatusAsync(string id)
    {
        var role = await roleManager.FindByIdAsync(id);

        if (role is null)
            return Result.Failure(RoleErrors.RoleNotFound);

        role.IsDeleted = !role.IsDeleted;

        await roleManager.UpdateAsync(role);

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(string id, RoleRequest request, CancellationToken cancellationToken = default)
    {
        var roleIsExists = await roleManager.Roles.AnyAsync(r => r.Name == request.Name && r.Id != id);

        if (roleIsExists)
            return Result.Failure<RoleDetailResponse>(RoleErrors.DuplicatedRoleTitle);

        var role = await roleManager.FindByIdAsync(id);

        if (role is null)
            return Result.Failure(RoleErrors.RoleNotFound);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermission);

        role.Name = request.Name;

        var result = await roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            var currentPermissions = await unitOfWork.RoleClaims.GetPermissionsByRoleIdAsync(role.Id);

            // new permissions
            var newPermissions = request.Permissions.Except(currentPermissions)
                .Select(x => new IdentityRoleClaim<string>
                {
                    RoleId = id,
                    ClaimType = Permissions.Type,
                    ClaimValue = x
                });

            await unitOfWork.RoleClaims.AddPermissionsAsync(newPermissions, cancellationToken);

            // old ones

            var removedPermissions = currentPermissions.Except(request.Permissions);

            await unitOfWork.RoleClaims.RemovePermissionsAsync(id, removedPermissions, cancellationToken);

            await unitOfWork.SaveAsync();

            
            return Result.Success();
        }

        var error = result.Errors.First();

        return Result.Failure<RoleDetailResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }
}
