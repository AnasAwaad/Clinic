using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IDoctorTimeSlotRepository : IGenericRepository<DoctorTimeSlot>
{
    Task<bool> IsTimeSlotOverlappingAsync(int scheduleId, TimeOnly startTime, TimeOnly endTime);
    Task<IEnumerable<DoctorTimeSlot>> GetAllForDayAsync(string day);
}
