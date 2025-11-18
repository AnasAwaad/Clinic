using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clinic.Application.DTOs.Appointment;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Application.Services;
public class AppointmentService(IUnitOfWork unitOfWork, IMapper mapper) : IAppointmentService
{

    public async Task<Result<PaginatedList<AppointmentListResponse>>> GetAllAsync(int pageNumber,int pageSize)
    {
        var items = unitOfWork.Appointments.GetAllQueryable()
            .ProjectTo<AppointmentListResponse>(mapper.ConfigurationProvider);

        var result = await PaginatedList<AppointmentListResponse>.CreateAsync(items, pageNumber, pageSize);

        return Result.Success(result);
    }

    public async Task<Result<List<AppointmentResponse>>> GetInRangeAsync(DateTime start,DateTime end)
    {
        if (end <= start)
            return Result.Failure<List<AppointmentResponse>>(CommonErrors.InvalidRange);

        const int MAX_RANGE_DAYS = 92;
        if ((end - start).TotalDays > MAX_RANGE_DAYS)
            return Result.Failure<List<AppointmentResponse>>(CommonErrors.RangeTooLarge);

        var query = unitOfWork.Appointments.QueryInRange(DateOnly.FromDateTime(start), DateOnly.FromDateTime(end));

        var list = await query
            .ProjectTo<AppointmentResponse>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result.Success(list);
    }



    public async Task<Result<IEnumerable<TimeSlotResponse>>> GetAvailableSlotsAsync(string day)
    {
        var dayExists = await unitOfWork.Schedules.DayIsExists(day);

        if (!dayExists)
            return Result.Failure<IEnumerable<TimeSlotResponse>>(DoctorScheduleErrors.InvalidDay);

        var slots = await unitOfWork.TimeSlots.GetAvailableSlotsByDayAsync(day);

        return Result.Success(mapper.Map<IEnumerable<TimeSlotResponse>>(slots));
    }
    public async Task<Result<AppointmentDetailsResponse>> GetAsync(int id)
    {
        var appointment =await unitOfWork.Appointments
            .GetByIdWithDetails(id)
            .ProjectTo<AppointmentDetailsResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (appointment is null)
            return Result.Failure<AppointmentDetailsResponse>(AppointmentErrors.AppointmentNotFound);

        return Result.Success(appointment);
    }

    public async Task<Result<List<AppointmentResponse>>> GetByUserAsync(string userId)
    {
        var query = unitOfWork.Appointments.GetAllByPatientId(userId);

        var response = await mapper.ProjectTo<AppointmentResponse>(query).ToListAsync();

        return Result.Success(response);
    }

    // create appointment by patient
    public async Task<Result<AppointmentResponse>> CreateAsync(AppointmentRequest request)
    {
        var doctor = await unitOfWork.Doctors.GetByIdAsync(request.DoctorId);

        if(doctor is null) 
            return Result.Failure<AppointmentResponse>(DoctorErrors.DoctorNotFound);

        var patient = await unitOfWork.Patients.GetByIdAsync(request.PatientId);

        if(patient is null)
            return Result.Failure<AppointmentResponse>(PatientErrors.PatientNotFound);

        var hasActiveAppointment = await unitOfWork.Appointments.HasActiveAppointmentAsync(patient.Id, request.Date);

        if(hasActiveAppointment)
            return Result.Failure<AppointmentResponse>(AppointmentErrors.ActiveAppointmentExists);


        var timeSlot = await unitOfWork.TimeSlots.GetByIdAndDayAsync(request.TimeSlotId,request.Date.DayOfWeek.ToString());

        if (timeSlot is null)
            return Result.Failure<AppointmentResponse>(DoctorScheduleErrors.TimeSlotNotFound);

        if(timeSlot.IsBooked)
            return Result.Failure<AppointmentResponse>(AppointmentErrors.TimeSlotAlreadyBooked);

        if(timeSlot.IsDeleted)
            return Result.Failure<AppointmentResponse>(DoctorScheduleErrors.TimeSlotNotFound);

        var appointment = mapper.Map<Appointment>(request);
        appointment.PatientId = patient.Id;
        timeSlot.IsBooked = true;
        timeSlot.BookedAt = DateTime.Now;


        await unitOfWork.Appointments.AddAsync(appointment);
        await unitOfWork.SaveAsync();

        return Result.Success(mapper.Map<AppointmentResponse>(appointment));
    }

    


    public async Task<Result> CancelAsync(string userId, int id)
    {
        var appointment = await unitOfWork.Appointments.GetByIdAndPatientAsync(id,userId);

        if (appointment == null)
            return Result.Failure(AppointmentErrors.AppointmentNotFound);

        if (appointment.Status == "Cancelled")
            return Result.Failure(AppointmentErrors.AlreadyCancelled);

        appointment.Status = "Cancelled";
        appointment.TimeSlot.IsBooked = false;
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteManyAsync(List<int> ids)
    {
        var idList = ids.Distinct().ToList();

        if(idList.Count == 0)
            return Result.Failure(AppointmentErrors.NoIdsProvided);

        await unitOfWork.Appointments.DeleteManyAsync(idList);

        return Result.Success();
    }


    public async Task<Result> DeleteAsync(int id)
    {
        var appointment = await unitOfWork.Appointments.GetByIdWithTimeSlotAsync(id);

        if (appointment == null)
            return Result.Failure(AppointmentErrors.AppointmentNotFound);

        appointment.IsDeleted = true;
        appointment.DeletedOn = DateTime.Now;
        appointment.TimeSlot.IsBooked = false;

        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(int id, AppointmentRequest request)
    {
        var appointment = await unitOfWork.Appointments.GetByIdWithTimeSlotAsync(id);

        if (appointment == null)
            return Result.Failure(AppointmentErrors.AppointmentNotFound);

        appointment.TimeSlot.IsBooked = false;

        mapper.Map(request, appointment);

        var timeSlot = await unitOfWork.TimeSlots.GetByIdAsync(request.TimeSlotId);

        if(timeSlot is null)
            return Result.Failure(DoctorScheduleErrors.TimeSlotNotFound);

        if (timeSlot.IsBooked)
            return Result.Failure(DoctorScheduleErrors.BookedTimeSlot);

        timeSlot.IsBooked = true;

        await unitOfWork.SaveAsync();
        return Result.Success();
    }
}
