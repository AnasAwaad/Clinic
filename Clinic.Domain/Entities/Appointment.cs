using Clinic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class Appointment : AuditableEntity
{
    public int Id { get; set; }
    public string PatientId { get; set; } = string.Empty;
    public string DoctorId { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public AppointmentStatus Status { get; set; }
    public int TimeSlotId { get; set; }
    public string ReasonForVisit { get; set; } = default!;
    public string VisitType { get; set; } = default!;

    public DateTime BookedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public DoctorTimeSlot TimeSlot { get; set; } = default!;
    public Patient Patient { get; set; } = default!;
    public Doctor Doctor { get; set; } = default!;
}
