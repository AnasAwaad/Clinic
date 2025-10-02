using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IRoleRepository
{
    Task<IList<string?>> GetUserPermissionsAsync(IEnumerable<string> roles, CancellationToken cancellationToken = default);
}
