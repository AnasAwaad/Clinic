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

    public Task<bool> IsTimeSlotOverlappingAsync(int scheduleId, TimeOnly startTime, TimeOnly endTime)
    {
        return context.Set<DoctorTimeSlot>()
            .AnyAsync(x => x.ScheduleId == scheduleId && ((x.StartTime <= startTime && x.EndTime <= endTime) ||
            (x.StartTime >= startTime && x.EndTime >= endTime) || (x.StartTime <= startTime && x.EndTime >= endTime) ||
            (x.StartTime >= startTime && x.EndTime <= endTime))); 
    }
}