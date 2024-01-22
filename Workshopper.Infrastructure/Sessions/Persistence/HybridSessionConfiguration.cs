using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Sessions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Sessions.Persistence;

internal class HybridSessionConfiguration : SessionConfiguration, IEntityTypeConfiguration<HybridSession>
{
    public void Configure(EntityTypeBuilder<HybridSession> builder)
    {
        builder.ToTable("hybrid_sessions", DatabaseSchema.Sessions);

        builder.Property(x => x.Link)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(250)
            .IsRequired();
    }
}