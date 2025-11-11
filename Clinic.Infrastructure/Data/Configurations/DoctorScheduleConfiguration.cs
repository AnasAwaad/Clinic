using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Configurations;
internal class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Day)
            .HasMaxLength(100);

        builder.HasData([
            new DoctorSchedule { Id = 1, Day = "Monday", DoctorId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e" },
            new DoctorSchedule { Id = 2, Day = "Tuesday", DoctorId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e" },
            new DoctorSchedule { Id = 3, Day = "Wednesday", DoctorId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e" },
            new DoctorSchedule { Id = 4, Day = "Thursday", DoctorId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e" },
            new DoctorSchedule { Id = 5, Day = "Friday", DoctorId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e" },
            new DoctorSchedule { Id = 6, Day = "Saturday", DoctorId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e" },
            new DoctorSchedule { Id = 7, Day = "Sunday", DoctorId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e" },

        ]);
    }
}