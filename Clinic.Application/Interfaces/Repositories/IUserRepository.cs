using Clinic.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IUserRepository : IGenericRepository<ApplicationUser>
{
    Task<IEnumerable<UserResponse>> GetAllUsersWithRoles();
    Task<List<string>> GetSecretaryUserIdsAsync();
}
