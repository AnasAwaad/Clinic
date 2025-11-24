using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Clinic;
public class ClinicSettingsRequest
{
    public string ClinicName { get; set; } = string.Empty;
    public string ClinicAddress { get; set; } = string.Empty;
    public string ClinicPhone { get; set; } = string.Empty;
    public WorkHours WorkHours { get; set; } = default!;

    public string DoctorName { get; set; } = string.Empty;
    public string DoctorDegree { get; set; } = string.Empty;
    public string DoctorRegNo { get; set; } = string.Empty;

    public IFormFile? Logo { get; set; } 
}
