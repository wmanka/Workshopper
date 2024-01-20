﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Subscriptions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Subscriptions.Persistence;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions", DatabaseSchema.Public);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired();
    }
}