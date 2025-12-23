namespace Clinic.Application.DTOs.Appointment;
public class AppointmentResponse
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public string ReasonForVisit { get; set; } = default!;
    public string VisitType { get; set; } = default!;
    public string Status { get; set; } = string.Empty;
}
