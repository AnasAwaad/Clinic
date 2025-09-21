using Clinic.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Data.Configurations;
public class PatientConfiguration:IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        // Primary Key
        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.DateOfBirth).IsRequired();
        builder.Property(p => p.Address).HasMaxLength(500);
        builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");

        // Index on UserId
        builder.HasIndex(p => p.UserId).IsUnique();

        // One-to-One with AspNetUsers
        builder.HasOne(p=>p.User)
               .WithOne()
               .HasForeignKey<Patient>(p => p.UserId)
               .IsRequired();
        
        // Global query filter for soft delete
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
