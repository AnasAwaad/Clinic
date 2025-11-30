using Clinic.Application.DTOs.Common;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Clinic.Application.DTOs.Result;


namespace Clinic.Infrastructure.Repositories;
internal class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(DbContext context) : base(context)
    {
    }


    public Task<Appointment?> GetByIdWithSlotAsync(int appointmentId)
    {
        return _context.Set<Appointment>()
            .Include(a => a.TimeSlot)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);
    }

    public IQueryable<Appointment> GetAllByPatientId(string userId)
    {
        return _context.Set<Appointment>()
            .Include(x => x.Doctor)
            .Include(x => x.TimeSlot)
            .Where(x => x.Patient.Id == userId);
    }

    public Task<bool> HasActiveAppointmentAsync(string patientId, DateOnly now)
    {
        return _context.Set<Appointment>()
            .AnyAsync(x => x.PatientId == patientId
                           && x.Date >= now
                           && x.Status == "Booked");
    }

    public IQueryable<Appointment> GetByIdWithDetails(int id)
    {
        return _context.Set<Appointment>()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.TimeSlot)
            .Where(a => a.Id == id);
    }

    public IQueryable<Appointment> GetAllQueryable(RequestFilters filters)
    {
        var query = _context.Set<Appointment>()
            .Where(a => !a.IsDeleted);

        if (!string.IsNullOrEmpty(filters.SearchValue))
        {
            var searchValue = filters.SearchValue.Trim();
            query = query.Where(x =>
                x.Patient.FullName.Contains(searchValue) ||
                x.Patient.PhoneNumber!.Contains(searchValue) ||
                x.Date.ToString().Contains(searchValue));
        }

        if (!string.IsNullOrEmpty(filters.SortColumn))
        {
            query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
        }

        return query
            .Include(a => a.Patient)
            .Include(a => a.TimeSlot)
            .AsQueryable();
    }


    public IQueryable<Appointment> QueryInRange(DateOnly start, DateOnly end)
    {
        return _context.Set<Appointment>()
            .AsNoTracking()
            .Include(a => a.Patient)
            .Include(a => a.TimeSlot)
            .Where(a => a.Date < end && a.Date> start)
            .OrderBy(a => a.Date)
            .ThenBy(a => a.TimeSlot.StartTime);

    }

    public async Task DeleteManyAsync(List<int> idList)
    {
        await using var transcation = await _context.Database.BeginTransactionAsync();

        // Soft-delete appointments 
        await _context.Set<Appointment>()
            .Where(a => idList.Contains(a.Id))
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.IsDeleted, true)
                .SetProperty(a => a.DeletedOn, DateTime.UtcNow));

        // Collect the related TimeSlot IDs
        var timeSlotIds = await _context.Set<Appointment>()
            .Where(a => idList.Contains(a.Id) && a.TimeSlot != null)
            .Select(a => a.TimeSlotId)
            .Distinct()
            .ToListAsync();

        if (timeSlotIds.Any())
        {
            await _context.Set<DoctorTimeSlot>()
                .Where(x => timeSlotIds.Contains(x.Id))
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.IsBooked, false));
        }

        await transcation.CommitAsync();
    }


    public async Task<Appointment?> GetByIdWithTimeSlotAsync(int appointmentId)
    {
        return await _context.Set<Appointment>()
            .Where(a => a.Id == appointmentId)
            .Include(a => a.TimeSlot)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<AppointmentsPerDayResponse>> GetAppointmentsPerDaysAsync()
    {
        return await _context.Set<Appointment>()
            .Where(x => !x.IsDeleted)
            .GroupBy(x => new { x.Date })
            .Select(x => new AppointmentsPerDayResponse
            {
                Date = x.Key.Date,
                Count = x.Count()
            }).ToListAsync();
    }
}
