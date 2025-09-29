using Clinic.Application.DTOs.Auth;
using Clinic.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterPatientDto dto);
    Task<Result<LoginResult>> LoginAsync(LoginDto dto);
    //Task<Result<LoginResult>> LoginWithGoogle(ClaimsPrincipal claimsPrincipal);
}
