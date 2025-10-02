using AutoMapper;
using Clinic.Application.DTOs.Appointment;
using Clinic.Application.DTOs.Auth;
using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.DTOs.Prescription;
using Clinic.Application.DTOs.Role;
using Clinic.Application.DTOs.User;
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


        // appointment
        CreateMap<Appointment, AppointmentResponse>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.User.FirstName} {src.Doctor.User.LastName}"))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.TimeSlot.StartTime.ToString("HH:mm tt")));


        CreateMap<Appointment, AppointmentDetailsResponse>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.Patient.User.FirstName} {src.Patient.User.LastName}"))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.TimeSlot.StartTime.ToString("HH:mm tt")))
            .ForMember(dest => dest.PatientPhoneNumber, opt => opt.MapFrom(src => src.Patient.User.PhoneNumber));

        CreateMap<AppointmentRequest, Appointment>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Booked"));


        // medical record

        CreateMap<MedicalRecord, MedicalRecordResponse>();
        CreateMap<MedicalRecordRequest, MedicalRecord>();

        // prescription
        CreateMap<Prescription, PrescriptionResponse>();
        CreateMap<PrescriptionRequest, Prescription>();

        //user
        CreateMap<ApplicationUser, UserProfileResponse>();
        CreateMap<ApplicationUser, UserResponse>();

        // auth
        CreateMap<RegisterRequest, ApplicationUser>();
        
        // role
        CreateMap<ApplicationRole, RoleResponse>();

    }

}
