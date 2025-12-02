namespace Clinic.Application.DTOs.Result;
public class AppointmentStatusResponse
{
    public int AllAppointmentsCount { get; set; }
    public int CancelledCount { get; set; }
    public int BookedCount { get; set; }
    public int CompletedCount { get; set; }
    public List<TimeSegmentStatsResponse> SegmentStatus { get; set; } = default!;
}
