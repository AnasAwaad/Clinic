using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Mapping;
public class DoctorScheduleMapping : Profile
{
    public DoctorScheduleMapping()
    {
        CreateMap<DoctorSchedule, DoctorScheduleResponse>()
            .ConstructUsing(src => new DoctorScheduleResponse(
                src.Id,
                src.Day,
                src.TimeSlots.First().StartTime,
                src.TimeSlots.First().EndTime,
                src.TimeSlots.First().IsBooked
            ));

    }

}
