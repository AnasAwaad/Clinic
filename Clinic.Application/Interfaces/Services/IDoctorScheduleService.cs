using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IDoctorScheduleService
{
    Task<Result<TimeSlotListResponse>> GetAllAsync(DateOnly? date);
    Task<Result<TimeSlotResponse>> GetByIdAsync(int id);
    Task<Result<TimeSlotResponse>> CreateAsync(string day, TimeSlotRequest request);
    Task<Result> UpdateAsync(int id, TimeSlotRequest request);
    Task<Result> DeleteAsync(int id);
}
