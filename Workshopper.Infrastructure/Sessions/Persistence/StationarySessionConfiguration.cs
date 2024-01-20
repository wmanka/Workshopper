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

        builder.Property(x => x.Address)
            .HasMaxLength(250)
            .IsRequired();
    }
}