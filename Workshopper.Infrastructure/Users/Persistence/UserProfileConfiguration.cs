using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Users.UserProfiles;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Users.Persistence;

internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles", DatabaseSchema.Public);

        builder.HasIndex(x => new
        {
            x.ProfileType,
            x.UserId
        }).IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.ProfileType)
            .HasConversion(
                profileType => profileType.Name,
                value => ProfileType.FromName(value, false))
            .IsRequired();

        builder.HasDiscriminator(x => x.ProfileType)
            .HasValue<AttendeeProfile>(ProfileType.Attendee)
            .HasValue<HostProfile>(ProfileType.Host);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.HasIndex(x => x.UserId)
            .IsUnique(false);

        builder.Property(x => x.FirstName)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(250)
            .IsRequired();
    }
}