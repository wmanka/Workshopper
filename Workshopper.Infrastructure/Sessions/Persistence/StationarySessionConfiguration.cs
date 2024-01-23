using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Sessions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Sessions.Persistence;

internal class StationarySessionConfiguration : SessionConfiguration, IEntityTypeConfiguration<StationarySession>
{
    public void Configure(EntityTypeBuilder<StationarySession> builder)
    {
        builder.ToTable("stationary_sessions", DatabaseSchema.Sessions);

        builder.OwnsOne(x => x.Address,
            b =>
            {
                b.ToTable("stationary_sessions_address", DatabaseSchema.Sessions);

                b.WithOwner();

                b.Property(x => x.Line1)
                    .HasMaxLength(250)
                    .IsRequired();

                b.Property(x => x.Line2)
                    .HasMaxLength(250)
                    .IsRequired(false);

                b.Property(x => x.City)
                    .HasMaxLength(250)
                    .IsRequired();

                b.Property(x => x.Country)
                    .HasMaxLength(250)
                    .IsRequired();

                b.Property(x => x.PostCode)
                    .HasMaxLength(50)
                    .IsRequired();
            });

        builder.Navigation(x => x.Address)
            .IsRequired();
    }
}