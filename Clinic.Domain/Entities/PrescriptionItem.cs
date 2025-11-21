using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class PrescriptionItem : AuditableEntity
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; 
    public string Dosage { get; set; } = string.Empty;
    public int Frequency { get; set; }
    public int Days { get; set; }
    public string? Instructions { get; set; }
    public int PrescriptionId { get; set; }
    public Prescription Prescription { get; set; } = default!;
}
