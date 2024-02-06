using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Notifications;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Notifications.Persistence;

internal class NotificationSubscriptionConfiguration : IEntityTypeConfiguration<NotificationSubscription>
{
    public void Configure(EntityTypeBuilder<NotificationSubscription> builder)
    {
        builder.ToTable("notification_subscriptions", DatabaseSchema.Notifications);

        builder.HasKey(x => new
        {
            x.NotificationType,
            x.NotificationDeliveryType,
            x.UserProfileId
        });

        builder.HasIndex(x => new
        {
            x.NotificationType,
            x.NotificationDeliveryType,
            x.UserProfileId
        }).IsUnique();

        builder.Property(x => x.NotificationType)
            .HasConversion(
                x => x.Name,
                x => NotificationType.FromName(x, false))
            .IsRequired();

        builder.Property(x => x.NotificationDeliveryType)
            .HasConversion(
                sessionType => sessionType.Name,
                value => NotificationDeliveryType.FromName(value, false))
            .IsRequired();

        builder.HasOne(x => x.UserProfile)
            .WithMany()
            .HasForeignKey(x => x.UserProfileId);
    }
}