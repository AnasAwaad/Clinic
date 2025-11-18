using Clinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IDoctorScheduleRepository : IGenericRepository<DoctorSchedule>
{
    Task<bool> DayIsExists(string day);
    Task<DoctorSchedule> GetByDayAsync(string day);
    Task<IEnumerable<DoctorSchedule>> GetAllWithTimesAsync(bool includeDeleted);
}
