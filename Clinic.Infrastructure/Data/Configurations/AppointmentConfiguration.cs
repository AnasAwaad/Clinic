using Clinic.Domain.Entities;
using Clinic.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Configurations;
internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(a => a.Status)
            .HasConversion(
                v => v.ToString(),
                v => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), v))
            .IsRequired();

        builder.Property(a => a.ReasonForVisit)
            .HasMaxLength(500);

        builder.Property(a => a.VisitType)
            .HasMaxLength(100);

    }
}
