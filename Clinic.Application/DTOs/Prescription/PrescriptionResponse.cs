namespace Clinic.Application.DTOs.Prescription;
public class PrescriptionResponse
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string Age { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string? NextVisit { get; set; }
    public string? Notes { get; set; }

    public PrescriptionPatientResponse Patient { get; set; } = default!;
    public ICollection<PrescriptionItemResponse> Items { get; set; } = [];
    
}
