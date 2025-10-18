using Clinic.Domain.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations;
internal class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        var claims = new List<IdentityRoleClaim<string>>();
        int id = 1;

        #region Admin Permissions
        var permissions = Permissions.GetAllPermissions();

        foreach(var permission in permissions)
        {
            claims.Add(new IdentityRoleClaim<string>
            {
                Id = id++,
                ClaimType = Permissions.Type,
                ClaimValue = permission,
                RoleId = "92787aec-1266-4a2d-8a2d-6ea48f5a4811" // Admin Role Id
            });
        }
        #endregion

        #region Doctor Permissions
        var doctorPermissions = new List<string>
        {
            Permissions.GetMedicalRecords,
            Permissions.AddMedicalRecords,
            Permissions.UpdateMedicalRecords,

            Permissions.GetPrescriptions,
            Permissions.AddPrescriptions,
            Permissions.UpdatePrescriptions,
            Permissions.DeletePrescriptions,

            Permissions.GetAppointments
        };

        foreach (var permission in doctorPermissions)
        {
            claims.Add(new IdentityRoleClaim<string>
            {
                Id = id++,
                RoleId = "0d1fe96c-7786-4ce6-8647-38da6886a662", // Doctor Role Id
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }
        #endregion

        #region Secretary Permissions
        
        var secretaryPermissions = new List<string>
        {
            Permissions.GetAppointments,
            Permissions.AddAppointments,
            Permissions.CancelAppointments,
            Permissions.GetTimeSlots,
            Permissions.GetUsers
        };

        foreach (var permission in secretaryPermissions)
        {
            claims.Add(new IdentityRoleClaim<string>
            {
                Id = id++,
                RoleId = "e6a5b8c2-6254-4279-866b-c916377576db",
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        #endregion

        #region Patient Permissions

        var patientPermissions = new List<string>
        {
            Permissions.AddAppointments,
            Permissions.GetOwnAppointments,
            Permissions.CancelAppointments,
            Permissions.GetTimeSlots
        };

        foreach (var permission in patientPermissions)
        {
            claims.Add(new IdentityRoleClaim<string>
            {
                Id = id++,
                RoleId = "f4c051d8-f995-492c-8ef8-515417105616", // Patient Role Id
                ClaimType = Permissions.Type,
                ClaimValue = permission
            });
        }

        #endregion


        builder.HasData(claims);
    }
}
