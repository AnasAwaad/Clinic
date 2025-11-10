using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories;
internal class DoctorTimeSlotRepository : GenericRepository<DoctorTimeSlot>, IDoctorTimeSlotRepository
{
    private readonly DbContext context;

    public DoctorTimeSlotRepository(DbContext context) : base(context)
    {
        this.context = context;
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

    public async Task<IEnumerable<DoctorTimeSlot>> GetByScheduleIdAsync(int scheduleId)
    {
        return await context.Set<DoctorTimeSlot>()
            .Where(x => x.ScheduleId == scheduleId)
            .OrderBy(x=> x.StartTime)
            .ToListAsync();
    }

    public void DeleteByScheduleId(int scheduleId)
    {
        var slots = context.Set<DoctorTimeSlot>().Where(x => x.ScheduleId == scheduleId);
        context.Set<DoctorTimeSlot>().RemoveRange(slots);
    }
}