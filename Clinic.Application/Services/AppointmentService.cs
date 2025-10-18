using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clinic.Application.DTOs.Appointment;
using Clinic.Application.DTOs.Prescription;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<Result<AppointmentResponse>> CreateAsync(string userId,AppointmentRequest request)
    {
        var doctor = await unitOfWork.Doctors.GetByIdAsync(request.DoctorId);

        if(doctor is null) 
            return Result.Failure<AppointmentResponse>(DoctorErrors.DoctorNotFound);

        var patient = await unitOfWork.Patients.GetByUserIdAsync(userId);

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

        var appointment = mapper.Map<Appointment>(request);
        appointment.PatientId = patient.Id;
        timeSlot.IsBooked = true;


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

}
