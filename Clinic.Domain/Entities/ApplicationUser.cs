using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = null!;
    public string? ImageUrl { get; set; }
}

