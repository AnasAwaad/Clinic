using Clinic.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId, CancellationToken cancellationToken = default);
    Task UpdateProfileAsync(string userId, UpdateProfileRequest request, CancellationToken cancellationToken = default);
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
}
