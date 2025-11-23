using Clinic.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.UseTptMappingStrategy();

        builder.Property(u => u.FullName).HasMaxLength(100);

        builder
            .OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");

        var hasher = new PasswordHasher<ApplicationUser>();

        var admin = new ApplicationUser
        {
            Id = "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
            FullName = "Admin",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "Admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = hasher.HashPassword(null!,"Pa$$w0rd")
        };
        //admin.PasswordHash = hasher.HashPassword(admin, "Pa$$w0rd");

        

        var secretary = new ApplicationUser
        {
            Id = "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
            FullName = "Secretary",
            UserName = "Secretary",
            NormalizedUserName = "SECRETARY",
            Email = "Secretary@gmail.com",
            NormalizedEmail = "SECRETARY@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        secretary.PasswordHash = hasher.HashPassword(secretary, "Secretary@123");

        builder.HasData(admin, secretary);
    }
}
