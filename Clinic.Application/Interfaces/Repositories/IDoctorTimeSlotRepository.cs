using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IDoctorTimeSlotRepository : IGenericRepository<DoctorTimeSlot>
{
    Task<bool> IsTimeSlotOverlappingAsync(int scheduleId, TimeOnly startTime, TimeOnly endTime);
    Task<DoctorTimeSlot?> GetByIdAndDayAsync(int timeSlotId, string day);
    Task<IEnumerable<DoctorTimeSlot>> GetAvailableSlotsByDayAsync(string day);
    Task<IEnumerable<DoctorTimeSlot>> GetByScheduleIdAsync(int scheduleId);
    void DeleteByScheduleId(int scheduleId);
}
