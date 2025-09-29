using AutoMapper;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
public class DoctorScheduleService(IUnitOfWork unitOfWork,IMapper mapper) : IDoctorScheduleService
{
    public async Task<Result<DoctorScheduleResponse>> CreateAsync(int doctorId, string day, DoctorScheduleRequest request)
    {
        var doctor =await unitOfWork.Doctors.GetByIdAsync(doctorId);

        if (doctor is null)
            return Result.Failure<DoctorScheduleResponse>(DoctorErrors.DoctorNotFound);

        var dayExists = await unitOfWork.Schedules.DayIsExists(day);

        if (!dayExists)
            return Result.Failure<DoctorScheduleResponse>(DoctorScheduleErrors.InvalidDay);

        var schedule = await unitOfWork.Schedules.GetByDayAsync(day);

        var startTime = TimeOnly.Parse(request.StartTime);
        var endTime = TimeOnly.Parse(request.EndTime);

        var isOverloapping = await unitOfWork.TimeSlots.IsTimeSlotOverlappingAsync(schedule.Id, startTime, endTime);
        if (isOverloapping)
            return Result.Failure<DoctorScheduleResponse>(DoctorScheduleErrors.OverlappingTimeSlot);

        var timeSlot =  new DoctorTimeSlot
        {
            ScheduleId = schedule.Id,
            StartTime = startTime,
            EndTime = endTime
        };

        await unitOfWork.TimeSlots.AddAsync(timeSlot);
        await unitOfWork.SaveAsync();

        return Result.Success(mapper.Map<DoctorScheduleResponse>(schedule));

    }
}
