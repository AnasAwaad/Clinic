using Clinic.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName).HasMaxLength(100);
        builder.Property(u => u.LastName).HasMaxLength(100);

        builder
            .OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");

        var hasher = new PasswordHasher<ApplicationUser>();

        var superAdmin = new ApplicationUser
        {
            Id = "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
            FirstName = "Super",
            LastName = "Admin",
            UserName = "SuperAdmin",
            NormalizedUserName = "SUPERADMIN",
            Email = "SuperAdmin@gmail.com",
            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        superAdmin.PasswordHash = hasher.HashPassword(superAdmin, "SuperAdmin@123");

        var doctor = new ApplicationUser
        {
            Id = "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
            FirstName = "Doctor",
            LastName = "",
            UserName = "Doctor",
            NormalizedUserName = "DOCTOR",
            Email = "Doctor@gmail.com",
            NormalizedEmail = "DOCTOR@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        doctor.PasswordHash = hasher.HashPassword(doctor, "Doctor@123");

        var secretary = new ApplicationUser
        {
            Id = "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
            FirstName = "Secretary",
            LastName = "",
            UserName = "Secretary",
            NormalizedUserName = "SECRETARY",
            Email = "Secretary@gmail.com",
            NormalizedEmail = "SECRETARY@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        secretary.PasswordHash = hasher.HashPassword(secretary, "Secretary@123");

        builder.HasData(superAdmin, doctor, secretary);
    }
}
