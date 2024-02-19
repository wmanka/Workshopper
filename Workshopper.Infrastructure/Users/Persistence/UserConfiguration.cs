using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Users.Persistence;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", DatabaseSchema.Public);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.Hash)
            .HasMaxLength(250)
            .IsRequired();

        builder.HasOne(x => x.HostProfile)
            .WithOne(x => x.User)
            .HasForeignKey<HostProfile>(x => x.UserId);

        builder.HasOne(x => x.AttendeeProfile)
            .WithOne(x => x.User)
            .HasForeignKey<AttendeeProfile>(x => x.UserId);

        builder.Property(x => x.ImageId)
            .IsRequired(false);

        builder.OwnsOne(x => x.UserSettings,
            own =>
            {
                own.ToTable("user_settings", DatabaseSchema.Public);

                own.WithOwner()
                    .HasPrincipalKey(x => x.Id)
                    .HasForeignKey(x => x.Id);

                own.Property(x => x.Id)
                    .HasColumnName("user_id")
                    .IsRequired()
                    .ValueGeneratedNever();

                own.Property(x => x.DefaultProfileType)
                    .HasConversion(
                        profileType => profileType != null ? profileType.Name : string.Empty,
                        value => ProfileType.FromName(value, false))
                    .IsRequired(false);
            });
    }
}