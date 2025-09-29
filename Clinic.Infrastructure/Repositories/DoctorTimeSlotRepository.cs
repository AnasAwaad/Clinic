using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
internal class DoctorTimeSlotRepository : GenericRepository<DoctorTimeSlot>, IDoctorTimeSlotRepository
{
    private readonly DbContext context;

    public DoctorTimeSlotRepository(DbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<DoctorTimeSlot>> GetAllForDayAsync(string day)
    {
        return await context.Set<DoctorTimeSlot>()
            .Where(x => x.Schedule.Day == day)
            .ToListAsync();
            
    }

    public async Task<DoctorTimeSlot?> GetByIdAndDayAsync(int timeSlotId, string day)
    {
        return await context.Set<DoctorTimeSlot>()
            .Include(x => x.Schedule)
            .FirstOrDefaultAsync(x => x.Id == timeSlotId && x.Schedule.Day == day);
    }

    public Task<bool> IsTimeSlotOverlappingAsync(int scheduleId, TimeOnly startTime, TimeOnly endTime)
    {
        return context.Set<DoctorTimeSlot>()
            .AnyAsync(x => x.ScheduleId == scheduleId && startTime < x.EndTime && endTime > x.StartTime); 
    }

    public async Task<IEnumerable<DoctorTimeSlot>> GetAvailableSlotsByDayAsync(string day)
    {
        return await context.Set<DoctorTimeSlot>()
            .Where(x => x.Schedule.Day == day && !x.IsBooked)
            .ToListAsync();
    }
}