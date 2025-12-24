using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public bool IsDisabled { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsOnline { get; set; }
    public DateTime? LastSeen { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public ICollection<Notification> Notifications { get; set; } = [];
}

