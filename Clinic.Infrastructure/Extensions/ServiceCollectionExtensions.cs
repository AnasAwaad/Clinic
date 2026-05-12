using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Infrastructure.Authorization.Filters;
using Clinic.Infrastructure.Data;
using Clinic.Infrastructure.Repositories;
using Clinic.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RemoteConnection")
            ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

        var clientId = configuration["Authentication:Google:ClientId"]
                           ?? throw new InvalidOperationException("Google ClientId missing");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

    services.AddDataProtection();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPrescriptionPdfGenerator, PrescriptionPdfGenerator>();

        services.AddScoped<IUserRsaKeyService, UserRsaKeyService>();
        services.AddScoped<IMessageCryptoService, MessageCryptoService>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddScoped<IGoogleAuthService>(_ => new GoogleAuthService(clientId));

        return services;
    }
}
