namespace Clinic.Application.DTOs.Prescription;
public class PrescriptionResponse
{
    public int Id { get; set; }
    public string PatientId { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string PatientAge { get; set; } = string.Empty;
    public string PatientGender { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public DateOnly NextVisit { get; set; } 
    public string? Notes { get; set; }
    public ICollection<PrescriptionItemResponse> Items { get; set; } = [];
    
}
