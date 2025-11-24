using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Prescription;
public class PrescriptionPdfModel
{
    public string ClinicName { get; set; }
    public string ClinicAddress { get; set; }
    public string ClinicPhone { get; set; }
    public string ClinicTiming { get; set; }
    public string ClinicLogo { get; set; }

    public string DoctorName { get; set; }
    public string DoctorDegrees { get; set; }
    public string DoctorRegNo { get; set; }

    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientGenderAge { get; set; }
    public string PatientAddress { get; set; }
    public string Diagnosis { get; set; } = "FEVER";

    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public string Notes { get; set; }
    public List<PrescriptionItemPdf> Items { get; set; } = new();

}

public class PrescriptionItemPdf
{
    public string Name { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public int Frequency { get; set; }
    public int Days { get; set; }
    public string? Instructions { get; set; }
}