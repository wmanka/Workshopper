using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Sessions;
using Workshopper.Infrastructure.Common;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Sessions.Persistence;

internal class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("sessions", DatabaseSchema.Sessions);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.DeliveryType)
            .HasConversion(
                deliveryType => deliveryType.Name,
                value => DeliveryType.FromName(value, false))
            .IsRequired();

        builder.Property(x => x.SessionType)
            .HasConversion(
                sessionType => sessionType.Name,
                value => SessionType.FromName(value, false))
            .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.StartDateTime)
            .IsRequired();

        builder.Property(x => x.EndDateTime)
            .IsRequired();

        builder.Property(x => x.Places)
            .IsRequired();

        builder.Property(x => x.IsCanceled)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(x => x.IsCanceled == false);
    }
}