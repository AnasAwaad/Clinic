using Clinic.Application.DTOs.Prescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Appointment;
public class AppointmentDetailsResponse
{
    public int Id { get; set; }
    public string PatientId { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public string Status { get; set; } = string.Empty;
    public int TimeSlotId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string ReasonForVisit { get; set; } = string.Empty;
    public string VisitType { get; set; } = string.Empty;
    public DateTime BookedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public AppointmentPatientResponse Patient { get; set; } = default!;
}
