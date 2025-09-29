using Clinic.Application.Interfaces.Services;
using Clinic.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        return services;
    } 
}
