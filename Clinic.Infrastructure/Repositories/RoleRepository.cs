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
internal class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
{
    private readonly DbSet<IdentityRole> context;

    public RoleRepository(DbContext context) : base(context)
    {
        this.context = context.Set<IdentityRole>();
    }

    public async Task<IList<string?>> GetUserPermissionsAsync(IEnumerable<string> roles, CancellationToken cancellationToken)
    {
        //return await _context.Set<IdentityRole>()
        //    .Join(_context.Set<IdentityRoleClaim<string>>(),
        //        r => r.Id,
        //        rc => rc.RoleId,
        //        (r, rc) => new { r.Name, rc.ClaimType, rc.ClaimValue })
        //    .Where(x => roles.Contains(x.Name) && x.ClaimType == Permissions.Type)
        //    .Select(x => x.ClaimValue)
        //    .Distinct()
        //    .ToListAsync(cancellationToken);

        return await (from r in _context.Set<ApplicationRole>()
                      join rc in _context.Set<IdentityRoleClaim<string>>()
                      on r.Id equals rc.RoleId
                      where roles.Contains(r.Name) && rc.ClaimType == Permissions.Type
                      select rc.ClaimValue
                      ).Distinct().ToListAsync(cancellationToken);
    }
}
