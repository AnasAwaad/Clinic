using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Prescription;
public class PrescriptionPdfModel
{
    public string ClinicName { get; set; } = "Care Clinic";
    public string ClinicAddress { get; set; } = "Kothrud, Pune - 411038.";
    public string ClinicPhone { get; set; } = "094233 80390";
    public string ClinicTiming { get; set; } = "Timing: 09:00 AM - 02:00 PM | Closed: Thursday";

    public string DoctorName { get; set; } = "Dr. Onkar Bhave";
    public string DoctorDegrees { get; set; } = "M.B.B.S., M.D., M.S.";
    public string DoctorRegNo { get; set; } = "Reg. No: 270988";

    public string PatientId { get; set; } = "14";
    public string PatientName { get; set; } = "DEMO PATIENT";
    public string PatientGenderAge { get; set; } = "M / 8 Y";
    public string PatientAddress { get; set; } = "KOTHRUD, PUNE";
    public string ReferredBy { get; set; } = "Dr. Demo";
    public string Diagnosis { get; set; } = "FEVER";

    public DateTime Date { get; set; } = DateTime.Today;
    public string Advice { get; set; } = "* DRINK BOILED WATER";
    public DateTime NextVisit { get; set; } = DateTime.Today.AddDays(15);

    public List<PrescriptionItemPdf> Items { get; set; } = new();

}

public class PrescriptionItemPdf
{
    public string Medicine { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
}