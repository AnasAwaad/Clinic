using Clinic.Application.DTOs.Appointment;
using Clinic.Application.DTOs.Common;
using Clinic.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IAppointmentService
{
    Task<Result<IEnumerable<TimeSlotResponse>>> GetAvailableSlotsAsync(string day);
    Task<Result<List<AppointmentResponse>>> GetInRangeAsync(DateTime start, DateTime end);
    Task<Result<PaginatedList<AppointmentListResponse>>> GetAllAsync(RequestFilters filters);
    Task<Result<List<AppointmentResponse>>> GetByUserAsync(string userId);
    Task<Result<AppointmentDetailsResponse>> GetAsync(int id);
    Task<Result<AppointmentResponse>> CreateAsync(AppointmentRequest request);
    Task<Result> CancelAsync(int id);
    Task<Result> CompleteAsync(int id);
    Task<Result> UpdateAsync(int id,AppointmentRequest request);
    Task<Result> DeleteManyAsync(List<int> ids);
    Task<Result> DeleteAsync(int id);

}
