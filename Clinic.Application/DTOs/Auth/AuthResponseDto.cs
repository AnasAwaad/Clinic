using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Auth;
public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
}
