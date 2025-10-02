using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IRoleClaimRepository : IGenericRepository<IdentityRoleClaim<string>>
{
    Task AddRangeAsync(IEnumerable<IdentityRoleClaim<string>> claims, CancellationToken cancellationToken = default);
    Task<List<string?>> GetPermissionsByRoleIdAsync(string roleId, CancellationToken cancellationToken = default);
    Task AddPermissionsAsync(IEnumerable<IdentityRoleClaim<string>> claims, CancellationToken cancellationToken = default);
    Task RemovePermissionsAsync(string roleId, IEnumerable<string> permissions, CancellationToken cancellationToken = default);


}
