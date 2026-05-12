using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Appointment;
public record BookAppointmentRequest (
    int TimeSlotId,
    DateOnly Date,
    string ReasonForVisit,
    string VisitType
);
