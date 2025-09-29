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

    public Task<bool> IsTimeSlotOverlappingAsync(int scheduleId, TimeOnly startTime, TimeOnly endTime)
    {
        return context.Set<DoctorTimeSlot>()
            .AnyAsync(x => x.ScheduleId == scheduleId && startTime < x.EndTime && endTime > x.StartTime); 
    }
}