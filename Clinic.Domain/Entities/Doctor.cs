using Clinic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class Doctor : AuditableEntity
{
    public int Id { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public int YearOfExperience { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
