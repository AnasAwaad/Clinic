using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Configurations;
internal class ClinicSettingsConfiguration : IEntityTypeConfiguration<ClinicSettings>
{
    public void Configure(EntityTypeBuilder<ClinicSettings> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}
