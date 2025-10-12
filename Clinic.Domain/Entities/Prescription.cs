using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class Prescription : AuditableEntity
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int? MedicalRecordId { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }
    public DateTime Date { get; set; }
    public int Age { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public DateTime NextVisit { get; set; }
    public string? Notes { get; set; }
    public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    public Patient Patient { get; set; } = default!;
}
