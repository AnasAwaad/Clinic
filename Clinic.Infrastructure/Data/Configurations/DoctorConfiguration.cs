using Clinic.Domain.Entities;
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

        builder.Property(p => p.Specialization).HasMaxLength(200);


        builder.HasData(new Doctor
        {
            Id = 1,
            Specialization = "Cardiology",
            YearOfExperience = 10,
            UserId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
            CreatedById = "556c1c99-2d3a-4988-a80a-46ab2f14ea71"
        });


    }
}
