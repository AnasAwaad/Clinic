using Clinic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class Patient:BaseEntity
{
    public int Id { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? Gender { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

}
