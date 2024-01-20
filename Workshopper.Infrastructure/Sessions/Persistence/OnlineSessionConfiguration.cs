using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Sessions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Sessions.Persistence;

internal class OnlineSessionConfiguration : SessionConfiguration, IEntityTypeConfiguration<OnlineSession>
{
    public void Configure(EntityTypeBuilder<OnlineSession> builder)
    {
        builder.ToTable("online_sessions", DatabaseSchema.Sessions);

        builder.Property(x => x.Link)
            .HasMaxLength(250)
            .IsRequired();
    }
}