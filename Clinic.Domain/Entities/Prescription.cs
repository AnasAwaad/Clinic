using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class Prescription : AuditableEntity
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public string MedicationName { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public DateTime Duration { get; set; }
    public string Notes { get; set; } = string.Empty;

    public MedicalRecord MedicalRecord { get; set; } = default!;
}
