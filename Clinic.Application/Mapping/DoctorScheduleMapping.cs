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
        CreateMap<DoctorTimeSlot, TimeSlotResponse>();
        CreateMap<TimeSlotRequest, DoctorTimeSlot>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.EndTime)));
    }

}
