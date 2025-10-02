using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Consts;
using Clinic.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
public class RoleClaimRepository : GenericRepository<IdentityRoleClaim<string>>, IRoleClaimRepository
{
    public RoleClaimRepository(ApplicationDbContext context) : base(context) { }

    public async Task AddRangeAsync(IEnumerable<IdentityRoleClaim<string>> claims, CancellationToken cancellationToken = default)
    {
        await _context.Set<IdentityRoleClaim<string>>().AddRangeAsync(claims, cancellationToken);
    }
    public async Task<List<string?>> GetPermissionsByRoleIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<IdentityRoleClaim<string>>()
            .Where(r => r.RoleId == roleId && r.ClaimType == Permissions.Type)
            .Select(r => r.ClaimValue)
            .ToListAsync(cancellationToken);
    }
    public async Task AddPermissionsAsync(IEnumerable<IdentityRoleClaim<string>> claims, CancellationToken cancellationToken = default)
    {
        await _context.Set<IdentityRoleClaim<string>>().AddRangeAsync(claims, cancellationToken);
    }

    public async Task RemovePermissionsAsync(string roleId, IEnumerable<string> permissions, CancellationToken cancellationToken = default)
    {
        var toRemove = await _context.Set<IdentityRoleClaim<string>>()
            .Where(x => x.RoleId == roleId &&
                        x.ClaimType == Permissions.Type &&
                        permissions.Contains(x.ClaimValue))
            .ToListAsync(cancellationToken);

        if (toRemove.Count > 0)
            _context.Set<IdentityRoleClaim<string>>().RemoveRange(toRemove);
    }
}
