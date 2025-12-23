using AutoMapper;
using Clinic.Application.DTOs.Appointment;
using Clinic.Application.DTOs.Auth;
using Clinic.Application.DTOs.Clinic;
using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.DTOs.Patient;
using Clinic.Application.DTOs.Prescription;
using Clinic.Application.DTOs.Role;
using Clinic.Application.DTOs.User;

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
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.TimeSlot.StartTime.ToString("hh:mm tt")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.TimeSlot.EndTime.ToString("hh:mm tt")));



        CreateMap<Appointment, AppointmentDetailsResponse>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.TimeSlot.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.TimeSlot.EndTime));

        CreateMap<Patient, AppointmentPatientResponse>();

        CreateMap<AppointmentRequest, Appointment>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Booked"));


        CreateMap<BookAppointmentRequest, Appointment>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Booked"));


        // medical record

        CreateMap<MedicalRecord, MedicalRecordResponse>();
        CreateMap<MedicalRecordRequest, MedicalRecord>();

        // prescription
        CreateMap<Prescription, PrescriptionResponse>();
        CreateMap<Patient, PrescriptionPatientResponse>();

        CreateMap<PrescriptionItem, PrescriptionItemResponse>();

        CreateMap<PrescriptionRequest, Prescription>();
        CreateMap<PrescriptionItemRequest, PrescriptionItem>();

        CreateMap<Prescription, PrescriptionListResponse>()
            .ForMember(x => x.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
            .ForMember(x => x.ImageUrl, opt => opt.MapFrom(src => src.Patient.ImageUrl));

        //user
        CreateMap<ApplicationUser, UserProfileResponse>();

        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<CreateUserRequest, ApplicationUser>()
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));

        CreateMap<UpdateUserRequest, ApplicationUser>();

        // auth
        CreateMap<RegisterRequest, ApplicationUser>()
            .ForMember(x => x.ImageUrl, opt => opt.MapFrom(src => "/uploads/user.png"));
        
        // role
        CreateMap<ApplicationRole, RoleResponse>();

        // appointment
        CreateMap<Appointment, AppointmentListResponse>()
            .ForMember(x => x.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.Patient.PhoneNumber))
            .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Patient.Email))
            .ForMember(x => x.Gender, opt => opt.MapFrom(src => src.Patient.Gender))
            .ForMember(x => x.Address, opt => opt.MapFrom(src => src.Patient.Address))
            .ForMember(x => x.ImageUrl, opt => opt.MapFrom(src => src.Patient.ImageUrl))
            .ForMember(x => x.StartTime, opt => opt.MapFrom(src => src.TimeSlot.StartTime))
            .ForMember(x => x.EndTime, opt => opt.MapFrom(src => src.TimeSlot.EndTime));


        // patient
        CreateMap<Patient, PatientResposne>()
            .ForMember(x => x.LastVisit, opt => opt.MapFrom(src => src.Appointments.Count() > 0 ? src.Appointments.OrderByDescending(a => a.Date).FirstOrDefault()!.Date.ToString() : ""));


        CreateMap<PatientRequest, Patient>()
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true));

        CreateMap<UpdatePatientRequest, Patient>();

        // clinic
        CreateMap<ClinicSettings, ClinicSettingsResponse>();
        CreateMap<ClinicSettingsRequest, ClinicSettings>();


        // appointment
        CreateMap<Appointment, AppointmentHistoryResponse>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.TimeSlot.StartTime));


        // patient
        CreateMap<Patient, PatientInfoResponse>()
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("yyyy-MM-dd") : null))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)))
            .ForMember(dest => dest.Status, opt => opt.Ignore());

        // prescription
        CreateMap<Prescription, PrescriptionHistoryResponse>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.PrescriptionId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MedicationItems, opt => opt.MapFrom(src => src.Items.Select(i => i.Name).ToList()));

    }

    // Helper method to calculate age
    private static int CalculateAge(DateOnly? dob)
    {
        if (!dob.HasValue) return 0;
        var today = DateTime.Today; // 2025
        var dobDateTime = new DateTime(dob.Value.Year, dob.Value.Month, dob.Value.Day); // 2003
        var age = today.Year - dobDateTime.Year; // 22
        if (dobDateTime.Date > today.AddYears(-age)) age--; // 2003 > (2025-22) ? age-- 
        return age;
    }
}
