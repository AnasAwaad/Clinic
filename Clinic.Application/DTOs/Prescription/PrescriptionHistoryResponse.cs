using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Prescription;
public class PrescriptionHistoryResponse
{
    public int PrescriptionId { get; set; }
    public string Date { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public IEnumerable<string> MedicationItems { get; set; } = new List<string>();
}
