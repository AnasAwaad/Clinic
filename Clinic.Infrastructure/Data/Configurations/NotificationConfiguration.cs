using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic.Infrastructure.Data.Configurations;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(200);

        builder.Property(x => x.Message)
               .HasMaxLength(1000);

        builder.Property(x => x.IsRead)
               .HasDefaultValue(false);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x=>x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
