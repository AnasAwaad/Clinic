using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class ClinicSettings
{
    public int Id { get; set; }
    public string? ClinicName { get; set; }
    public string? ClinicAddress { get; set; }
    public string? ClinicPhone { get; set; }
    public WorkHours WorkHours { get; set; } = default!;

    public string? DoctorName { get; set; }
    public string? DoctorDegree { get; set; }
    public string? DoctorRegNo { get; set; }

    public string? LogoUrl { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
