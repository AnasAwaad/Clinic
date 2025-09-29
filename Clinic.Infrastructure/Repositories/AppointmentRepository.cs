using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
internal class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(DbContext context) : base(context)
    {
    }


    public Task<Appointment?> GetByIdAndPatientAsync(int appointmentId, string userId)
    {
        return _context.Set<Appointment>()
            .Include(a=>a.TimeSlot)
            .FirstOrDefaultAsync(a => a.Id == appointmentId && a.Patient.UserId == userId);
    }

    public IQueryable<Appointment> GetAllByPatientId(string userId)
    {
        return _context.Set<Appointment>()
            .Include(x => x.Doctor)
            .ThenInclude(x => x.User)
            .Include(x => x.TimeSlot)
            .Where(x => x.Patient.UserId == userId);
    }

    public Task<bool> HasActiveAppointmentAsync(int patientId, DateOnly now)
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
            .ThenInclude(d => d.User)
            .Include(a => a.TimeSlot)
            .Where(a => a.Id == id);
    }

}
