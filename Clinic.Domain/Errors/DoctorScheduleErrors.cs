using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Errors;
public static class DoctorScheduleErrors
{
    public static readonly Error InvalidTimeRange = new("DoctorSchedule.InvalidTimeRange", "Start time must be before end time.", StatusCodes.Status400BadRequest);
    public static readonly Error InvalidDay = new("DoctorSchedule.InvalidDay", "Invalid day", StatusCodes.Status400BadRequest);
    public static readonly Error OverlappingTimeSlot = new("DoctorSchedule.OverlappingTimeSlot", "the time slot overlaps with an existing slot", StatusCodes.Status400BadRequest);
}
