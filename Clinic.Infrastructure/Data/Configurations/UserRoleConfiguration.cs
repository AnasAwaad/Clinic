using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations;
public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {

        // Default Data
        builder.HasData(new IdentityUserRole<string>
        {
            UserId = "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
            RoleId = "92787aec-1266-4a2d-8a2d-6ea48f5a4811"
        },
        new IdentityUserRole<string>
        {
            UserId = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
            RoleId = "0d1fe96c-7786-4ce6-8647-38da6886a662",
        },
        new IdentityUserRole<string>
        {
            UserId = "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
            RoleId = "e6a5b8c2-6254-4279-866b-c916377576db"
        });

    }
}