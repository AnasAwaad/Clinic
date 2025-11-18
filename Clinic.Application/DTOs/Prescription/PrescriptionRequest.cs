using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Prescription;
public class PrescriptionRequest
{
    public string PatientId { get; set; }
    public DateOnly Date { get; set; }
    public int Age { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public ICollection<PrescriptionItemRequest> Items { get; set; } = [];
}
