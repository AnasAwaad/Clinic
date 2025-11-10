using AutoMapper;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
public class DoctorScheduleService(IUnitOfWork unitOfWork, IMapper mapper) : IDoctorScheduleService
{
    public async Task<Result<TimeSlotResponse>> CreateAsync(string day, TimeSlotRequest request)
    {
        var dayExists = await unitOfWork.Schedules.DayIsExists(day);

        if (!dayExists)
            return Result.Failure<TimeSlotResponse>(DoctorScheduleErrors.InvalidDay);

        var schedule = await unitOfWork.Schedules.GetByDayAsync(day);

        if(schedule is null)
            return Result.Failure<TimeSlotResponse>(DoctorScheduleErrors.ScheduleNotFound);

        var startTime = TimeOnly.Parse(request.StartTime);
        var endTime = TimeOnly.Parse(request.EndTime);

        var isOverloapping = await unitOfWork.TimeSlots.IsTimeSlotOverlappingAsync(schedule.Id, startTime, endTime);
        if (isOverloapping)
            return Result.Failure<TimeSlotResponse>(DoctorScheduleErrors.OverlappingTimeSlot);

        var timeSlot =  new DoctorTimeSlot
        {
            ScheduleId = schedule.Id,
            StartTime = startTime,
            EndTime = endTime
        };

        await unitOfWork.TimeSlots.AddAsync(timeSlot);
        await unitOfWork.SaveAsync();

        return Result.Success(mapper.Map<TimeSlotResponse>(timeSlot));

    }
    public async Task<Result<TimeSlotListResponse>> GetAllAsync(DateOnly? date)
    {
        var slots = await unitOfWork.Schedules.GetAllWithTimesAsync(date);

        var result = mapper.Map<IEnumerable<DaySlotResponse>>(slots);

        return Result.Success(new TimeSlotListResponse { Slots = result});
    } 
    public async Task<Result> DeleteAsync(int id)
    {
        var timeSlot = await unitOfWork.TimeSlots.GetByIdAsync(id);
        if (timeSlot is null)
            return Result.Failure(DoctorScheduleErrors.TimeSlotNotFound);

        await unitOfWork.TimeSlots.DeleteAsync(timeSlot);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<TimeSlotResponse>> GetByIdAsync(int id)
    {
        var timeSlot = await unitOfWork.TimeSlots.GetByIdAsync(id);
        if (timeSlot is null)
            return Result.Failure<TimeSlotResponse>(DoctorScheduleErrors.TimeSlotNotFound);

        var response = mapper.Map<TimeSlotResponse>(timeSlot);
        return Result.Success(response);
    }

    public async Task<Result> UpdateAsync(int id, TimeSlotRequest request)
    {
        var timeSlot = await unitOfWork.TimeSlots.GetByIdAsync(id);
        if (timeSlot is null)
            return Result.Failure<TimeSlotResponse>(DoctorScheduleErrors.TimeSlotNotFound);

        timeSlot = mapper.Map(request, timeSlot);

        await unitOfWork.SaveAsync();
        return Result.Success();
    }
}
