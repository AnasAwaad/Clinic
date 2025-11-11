using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class DoctorSchedule 
{
    public int Id { get; set; }
    public string Day { get; set; } = string.Empty;
    public string DoctorId { get; set; } = string.Empty;

    public Doctor Doctor { get; set; } = default!;
    public ICollection<DoctorTimeSlot> TimeSlots { get; set; } = new List<DoctorTimeSlot>();
}
