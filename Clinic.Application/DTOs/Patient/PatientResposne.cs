using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Patient;
public class PatientResposne
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? LastVisit { get; set; }
    public string? ImageUrl { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? Gender { get; set; }
}
