using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Subscriptions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Subscriptions.Persistence;

internal class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions", DatabaseSchema.Public);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.SubscriptionType)
            .HasConversion(
                subscriptionType => subscriptionType.Name,
                value => SubscriptionType.FromName(value, false))
            .IsRequired();
    }
}