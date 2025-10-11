using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.DoctorSchedules;
public class DaySlotResponse
{
    public string Day { get; set; } = string.Empty;
    public List<TimeSlotResponse> TimeSlots { get; set; } = default!;
}
