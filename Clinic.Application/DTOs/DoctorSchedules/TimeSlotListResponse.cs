using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.DoctorSchedules;
public class TimeSlotListResponse
{
    public IEnumerable<DaySlotResponse> Slots { get; set; } = default!;
}
