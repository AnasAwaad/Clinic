using AutoMapper;
using Clinic.Application.DTOs.Appointment;
using Clinic.Application.DTOs.Auth;
using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.DTOs.Patient;
using Clinic.Application.DTOs.Prescription;
using Clinic.Application.DTOs.Role;
using Clinic.Application.DTOs.User;
using Clinic.Application.Mapping.Resolvers;

namespace Clinic.Application.Mapping;
public class DoctorScheduleMapping : Profile
{
    public DoctorScheduleMapping()
    {
        CreateMap<DoctorSchedule, DaySlotResponse>();
        CreateMap<DoctorTimeSlot, TimeSlotResponse>();
        CreateMap<TimeSlotRequest, DoctorTimeSlot>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.EndTime)));


        // appointment
        CreateMap<Appointment, AppointmentResponse>()
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.TimeSlot.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.TimeSlot.EndTime))
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Patient.ImageUrl));



        CreateMap<Appointment, AppointmentDetailsResponse>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.Patient.FirstName}   {src.Patient.LastName}"))
            .ForMember(dest => dest.PatientPhoneNumber, opt => opt.MapFrom(src => src.Patient.PhoneNumber))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.TimeSlot.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.TimeSlot.EndTime));

        CreateMap<AppointmentRequest, Appointment>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Booked"));


        // medical record

        CreateMap<MedicalRecord, MedicalRecordResponse>();
        CreateMap<MedicalRecordRequest, MedicalRecord>();

        // prescription
        CreateMap<Prescription, PrescriptionResponse>()
            .ForMember(x => x.PatientName, opt => opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"));

        CreateMap<PrescriptionItem, PrescriptionItemResponse>();

        CreateMap<PrescriptionRequest, Prescription>();
        CreateMap<PrescriptionItemRequest, PrescriptionItem>();

        CreateMap<Prescription, PrescriptionListResponse>()
            .ForMember(x => x.PatientName, opt => opt.MapFrom(src => $"{src.Patient.FirstName}   {src.Patient.LastName}"))
            .ForMember(x => x.ImageUrl, opt => opt.MapFrom(src => src.Patient.ImageUrl));

        //user
        CreateMap<ApplicationUser, UserProfileResponse>();

        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<CreateUserRequest, ApplicationUser>()
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));
        CreateMap<UpdateUserRequest, ApplicationUser>();

        // auth
        CreateMap<RegisterRequest, ApplicationUser>();
        
        // role
        CreateMap<ApplicationRole, RoleResponse>();

        // appointment
        CreateMap<Appointment, AppointmentListResponse>()
            .ForMember(x => x.PatientName, opt => opt.MapFrom(src => $"{src.Patient.FirstName}   {src.Patient.LastName}"))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.Patient.PhoneNumber))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Patient.Email))
            .ForMember(x => x.Gender, opt => opt.MapFrom(src => src.Patient.Gender))
            .ForMember(x => x.Address, opt => opt.MapFrom(src => src.Patient.Address))
            .ForMember(x => x.ImageUrl, opt => opt.MapFrom(src => src.Patient.ImageUrl))
            .ForMember(x => x.StartTime, opt => opt.MapFrom(src => src.TimeSlot.StartTime))
            .ForMember(x => x.EndTime, opt => opt.MapFrom(src => src.TimeSlot.EndTime));


        // patient
        CreateMap<Patient, PatientResposne>()
            .ForMember(x => x.LastVisit,opt => opt.MapFrom(src=>src.Appointments.Count() > 0 ? src.Appointments.OrderByDescending(a=>a.Date).FirstOrDefault()!.Date.ToString() : ""))
            .ForMember(x=>x.ImageUrl,opt=>opt.MapFrom<PatientImageResolver>());


        CreateMap<PatientRequest, Patient>()
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));

        CreateMap<UpdatePatientRequest, Patient>();


    }

}
