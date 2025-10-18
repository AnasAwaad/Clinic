using Clinic.Application.DTOs.Appointment;
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
    Task<Result<PaginatedList<AppointmentListResponse>>> GetAllAsync(int pageNumber, int pageSize);
    Task<Result<List<AppointmentResponse>>> GetByUserAsync(string userId);
    Task<Result<AppointmentDetailsResponse>> GetAsync(int id);
    Task<Result<AppointmentResponse>> CreateAsync(string userId,AppointmentRequest request);
    Task<Result> CancelAsync(string userId,int id);

}
