using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.DoctorSchedules;
public record DoctorScheduleResponse(
    int Id,
    string Day,
    TimeOnly StartTime,
    TimeOnly EndTime,
    bool IsBooked
);