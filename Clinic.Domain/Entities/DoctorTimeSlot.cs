using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class DoctorTimeSlot
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateTime? BookedAt { get; set; }
    public bool IsBooked { get; set; }
    public bool IsDeleted { get; set; }
    public DoctorSchedule Schedule { get; set; } = default!;
}
