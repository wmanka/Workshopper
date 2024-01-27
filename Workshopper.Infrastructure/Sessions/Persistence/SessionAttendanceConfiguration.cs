using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Sessions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Sessions.Persistence;

internal class SessionAttendanceConfiguration : IEntityTypeConfiguration<SessionAttendance>
{
    public void Configure(EntityTypeBuilder<SessionAttendance> builder)
    {
        builder.ToTable("session_attendances", DatabaseSchema.Sessions);

        builder.Property(x => x.SessionId)
            .IsRequired();

        builder.Property(x => x.AttendeeProfileId)
            .IsRequired();

        builder.Property(x => x.IsCanceled)
            .HasDefaultValue(false)
            .IsRequired();
    }
}