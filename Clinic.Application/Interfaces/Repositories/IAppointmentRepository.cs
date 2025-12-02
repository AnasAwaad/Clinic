using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Result;
using Clinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    IQueryable<Appointment> GetAllByPatientId(string userId);
    IQueryable<Appointment> GetAllQueryable(RequestFilters filters);
    IQueryable<Appointment> GetAllQueryable();
    IQueryable<Appointment> QueryInRange(DateOnly start, DateOnly end);
    Task<bool> HasActiveAppointmentAsync(string patientId, DateOnly now);
    Task<Appointment?> GetByIdWithSlotAsync(int appointmentId);
    Task<Appointment?> GetByIdWithTimeSlotAsync(int appointmentId);
    IQueryable<Appointment> GetByIdWithDetails(int id);
    Task<IEnumerable<AppointmentsPerDayResponse>> GetAppointmentsPerDaysAsync();
    Task DeleteManyAsync(List<int> idList);
}
