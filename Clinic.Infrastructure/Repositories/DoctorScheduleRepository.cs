using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;

internal class DoctorScheduleRepository : GenericRepository<DoctorSchedule>, IDoctorScheduleRepository
{
    private readonly DbSet<DoctorSchedule> context;

    public DoctorScheduleRepository(DbContext context) : base(context)
    {
        this.context = context.Set<DoctorSchedule>();
    }

    public async Task<bool> DayIsExists(string day)
    {
        return await context.AnyAsync(ds => ds.Day == day);
    }

    public async Task<IEnumerable<DoctorSchedule>> GetAllWithTimesAsync(bool includeDeleted)
    {
        var query = context
           .AsNoTracking()
           .Select(ds => new DoctorSchedule
           {
               Day = ds.Day,
               TimeSlots = ds.TimeSlots
                .Where(ts => includeDeleted || !ts.IsDeleted)
                .ToList()
           });

        return await query
            .Where(ds => ds.TimeSlots.Any())
            .ToListAsync();
    }

    public async Task<DoctorSchedule> GetByDayAsync(string day)
    {
        return await context
            .Where(x => x.Day == day)
            .SingleAsync();
    }
}
