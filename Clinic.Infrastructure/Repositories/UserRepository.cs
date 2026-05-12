using Clinic.Application.DTOs.User;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Consts;
using Clinic.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }

    // get all users with roles except patient role
    public async Task<IEnumerable<UserResponse>> GetAllUsersWithRoles()
    {
        return await (from u in _context.Set<ApplicationUser>()
                join ur in _context.Set<IdentityUserRole<string>>()
                on u.Id equals ur.UserId
                join r in _context.Set<ApplicationRole>()
                on ur.RoleId equals r.Id into roles
                where !roles.Any(x => x.Name == "Patient") && !u.IsDeleted
                select new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.UserName,
                    u.PhoneNumber,
                    u.IsDisabled,
                    u.LockoutEnd,
                    Roles = roles.Select(r => r.Name).ToList()
                })
                .GroupBy(u => new { u.Id, u.FullName, u.Email, u.UserName, u.PhoneNumber ,u.IsDisabled,u.LockoutEnd})
                .Select(u => new UserResponse
                {
                    Id = u.Key.Id,
                    FullName = u.Key.FullName,
                    Email = u.Key.Email,
                    UserName = u.Key.UserName,
                    PhoneNumber = u.Key.PhoneNumber,
                    IsDisabled = u.Key.IsDisabled,
                    IsLocked = u.Key.LockoutEnd.HasValue && u.Key.LockoutEnd > DateTimeOffset.Now,
                    Roles = u.SelectMany(x => x.Roles)
                }).ToListAsync();
                
    }

    public async Task<List<string>> GetSecretaryUserIdsAsync()
    {
        return await (from u in _context.Set<ApplicationUser>()
                      join ur in _context.Set<IdentityUserRole<string>>() on u.Id equals ur.UserId
                      join r in _context.Set<ApplicationRole>() on ur.RoleId equals r.Id into roles
                      where roles.Any(x => x.Name == AppRoles.Secretary) && !u.IsDisabled && !u.IsDeleted
                      select u.Id)
                .ToListAsync();
    }
}
