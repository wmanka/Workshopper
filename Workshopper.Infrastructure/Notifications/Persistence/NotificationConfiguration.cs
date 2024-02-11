using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Notifications;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Notifications.Persistence;

internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications", DatabaseSchema.Notifications);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(1000)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.NotificationType)
            .HasConversion(
                x => x.Name,
                x => NotificationType.FromName(x, false))
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IsRead)
            .HasDefaultValue(false)
            .IsRequired();
    }
}