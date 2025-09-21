using Clinic.Domain.Consts;
using Clinic.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Seeds;
public static class DefaultUsers
{
    public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
    {
        var superAdmin = new ApplicationUser
        {
            FullName = "Super Admin",
            Email = "SuperAdmin@gmail.com",
            UserName = "SuperAdmin",
            EmailConfirmed = true,
        };

        var admin = new ApplicationUser
        {
            FullName = "Admin",
            Email = "Admin@gmail.com",
            UserName = "Admin",
            EmailConfirmed = true,
        };

        var secretary = new ApplicationUser
        {
            FullName = "Secretary",
            Email = "Secretary@gmail.com",
            UserName = "Secretary",
            EmailConfirmed = true,
        };

        var patient = new ApplicationUser
        {
            FullName = "Patient",
            Email = "Patient@gmail.com",
            UserName = "patient",
            EmailConfirmed = true,
        };

        var superAdminUser = await userManager.FindByEmailAsync(superAdmin.Email);
        var adminUser = await userManager.FindByEmailAsync(admin.Email);
        var secretaryUser = await userManager.FindByEmailAsync(secretary.Email);
        var patientUser = await userManager.FindByEmailAsync(patient.Email);

        if (superAdminUser is null)
        {
            await userManager.CreateAsync(superAdmin, "Password1!");
            await userManager.AddToRoleAsync(superAdmin, AppRoles.SuperAdmin);
            
        }

        if (adminUser is null)
        {
            await userManager.CreateAsync(admin, "Password1!");
            await userManager.AddToRoleAsync(admin, AppRoles.Admin);
        }

        if (secretaryUser is null)
        {
            await userManager.CreateAsync(secretary, "Password1!");
            await userManager.AddToRoleAsync(secretary, AppRoles.Secretary);
        }

        if (patientUser is null)
        {
            await userManager.CreateAsync(patient, "Password1!");
            await userManager.AddToRoleAsync(patient, AppRoles.Patient);
        }
    }
}