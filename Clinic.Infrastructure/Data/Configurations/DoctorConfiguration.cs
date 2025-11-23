using Clinic.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Configurations;
internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasBaseType<ApplicationUser>();

        builder.Property(p => p.Specialization).HasMaxLength(200);

        var hasher = new PasswordHasher<ApplicationUser>();

        var doctor = new Doctor
        {
            Id = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
            FullName = "Doctor",
            UserName = "Doctor",
            NormalizedUserName = "DOCTOR",
            Email = "Doctor@gmail.com",
            NormalizedEmail = "DOCTOR@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Specialization = "Cardiology",
            YearOfExperience = 10,

        };
        doctor.PasswordHash = hasher.HashPassword(doctor, "Pa$$w0rd");

        builder.HasData(doctor);


    }
}
