namespace Clinic.Application.DTOs.Result;
public class TimeSegmentStatsResponse
{
    public string Label { get; set; } = string.Empty;
    public int Completed { get; set; }
    public int Booked { get; set; }
    public int Cancelled { get; set; }
}
