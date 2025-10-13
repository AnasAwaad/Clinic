using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Prescription;
public class PrescriptionListResponse
{
    public int Id { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public int Age { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public DateOnly NextVisit { get; set; }
    public string? Notes { get; set; }
}
