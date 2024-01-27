using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Sessions;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Infrastructure.Users.Persistence;

internal class AttendeeProfileConfiguration : UserProfileConfiguration, IEntityTypeConfiguration<AttendeeProfile>
{
    public void Configure(EntityTypeBuilder<AttendeeProfile> builder)
    {
        builder.HasOne(x => x.User)
            .WithOne(x => x.AttendeeProfile)
            .HasForeignKey<User>(x => x.AttendeeProfileId);

        builder.HasMany(x => x.AttendedSessions)
            .WithMany(x => x.Attendees)
            .UsingEntity<SessionAttendance>();

        builder.Navigation(x => x.AttendedSessions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}