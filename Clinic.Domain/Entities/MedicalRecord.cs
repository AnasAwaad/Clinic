using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class MedicalRecord : AuditableEntity
{
    public int Id { get; set; }
    public string PatientId { get; set; } = string.Empty;
    public string DoctorId { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Doctor Doctor { get; set; } = default!;
    public Patient Patient { get; set; } = default!;
}
