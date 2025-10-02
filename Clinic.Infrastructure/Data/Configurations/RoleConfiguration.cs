using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations;
internal class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {

        // Default Data
        builder.HasData([
            new ApplicationRole{
                Id = "92787aec-1266-4a2d-8a2d-6ea48f5a4811",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "aee5e2f5-46b1-4ff7-8520-23fb69d75abd",
            },
            new ApplicationRole{
                Id = "0d1fe96c-7786-4ce6-8647-38da6886a662",
                Name = "Doctor",
                NormalizedName = "DOCTOR",
                ConcurrencyStamp = "1166ed41-1fa1-4d1f-9b6e-251a1706d31f",
            },
            new ApplicationRole{
                Id = "e6a5b8c2-6254-4279-866b-c916377576db",
                Name = "Secretary",
                NormalizedName = "SECRETARY",
                ConcurrencyStamp = "0da76d1d-fcd3-41cd-bdf7-44f17e6e75e2",
            },
            new ApplicationRole{
                Id = "f4c051d8-f995-492c-8ef8-515417105616",
                Name = "Patient",
                NormalizedName = "PATIENT",
                ConcurrencyStamp = "09e9d343-f279-48ad-98a1-96a37468d186",
                IsDefault = true
            }
        ]);
    }

}
