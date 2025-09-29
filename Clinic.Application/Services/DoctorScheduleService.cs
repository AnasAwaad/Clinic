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

        if (TimeOnly.Parse(request.StartTime) >= TimeOnly.Parse(request.EndTime))
            return Result.Failure<DoctorScheduleResponse>(DoctorErrors.DoctorNotFound);

        var schedule = new DoctorSchedule
        {
            Day = day,
            DoctorId = doctorId,
        };

        schedule.TimeSlots.Add(new DoctorTimeSlot 
        { 
            StartTime = TimeOnly.Parse(request.StartTime),
            EndTime = TimeOnly.Parse(request.EndTime)
        });

        await unitOfWork.Schedules.AddAsync(schedule);
        await unitOfWork.SaveAsync();

        return Result.Success(mapper.Map<DoctorScheduleResponse>(schedule));

    }
}
