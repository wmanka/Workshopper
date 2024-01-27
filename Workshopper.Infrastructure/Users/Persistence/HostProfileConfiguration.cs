using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Infrastructure.Users.Persistence;

internal class HostProfileConfiguration : UserProfileConfiguration, IEntityTypeConfiguration<HostProfile>
{
    public void Configure(EntityTypeBuilder<HostProfile> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.Company)
            .HasMaxLength(250)
            .IsRequired(false);

        builder.Property(x => x.Bio)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.IsVerified)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithOne(x => x.HostProfile)
            .HasForeignKey<User>(x => x.HostProfileId);

        builder.HasMany(x => x.HostedSessions)
            .WithOne(x => x.HostProfile)
            .HasForeignKey(x => x.HostProfileId);

        builder.Navigation(x => x.HostedSessions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}