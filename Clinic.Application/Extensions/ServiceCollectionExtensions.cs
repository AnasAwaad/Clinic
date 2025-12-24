using Clinic.Application.Interfaces.Services;
using Clinic.Application.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Clinic.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddAutoMapper(applicationAssembly);

        services
           .AddFluentValidationAutoValidation()
           .AddValidatorsFromAssembly(applicationAssembly);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDoctorScheduleService, DoctorScheduleService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IMedicalRecordService, MedicalRecordService>();
        services.AddScoped<IPrescriptionService, PrescriptionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IResultService, ResultService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IEmailSender, EmailService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IClinicSettingsService, ClinicSettingsService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<INotificationService, NotificationService>();

        services.AddHttpContextAccessor();



        return services;
    } 
}
