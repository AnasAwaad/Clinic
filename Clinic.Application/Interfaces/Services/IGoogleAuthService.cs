using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clinic.Application.Interfaces.Services;
public interface IGoogleAuthService
{
    public record GoogleUserDto(string Subject, string Email, string FullName, string Picture);

    Task<GoogleUserDto?> ValidateIdTokenAsync(string idToken);
}
