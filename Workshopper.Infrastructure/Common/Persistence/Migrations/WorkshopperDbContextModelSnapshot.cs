﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Workshopper.Infrastructure.Common.Persistence;

#nullable disable

namespace Workshopper.Infrastructure.Common.Persistence.Migrations
{
    [DbContext(typeof(WorkshopperDbContext))]
    partial class WorkshopperDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Workshopper.Domain.Sessions.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("DeliveryType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("delivery_type");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("description");

                    b.Property<DateTimeOffset>("EndDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date_time");

                    b.Property<Guid>("HostProfileId")
                        .HasColumnType("uuid")
                        .HasColumnName("host_profile_id");

                    b.Property<bool>("IsCanceled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_canceled");

                    b.Property<int>("Places")
                        .HasColumnType("integer")
                        .HasColumnName("places");

                    b.Property<string>("SessionType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("session_type");

                    b.Property<DateTimeOffset>("StartDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date_time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("HostProfileId")
                        .HasDatabaseName("ix_sessions_host_profile_id");

                    b.ToTable("sessions", "sessions");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.SessionAttendance", b =>
                {
                    b.Property<Guid>("AttendeeProfileId")
                        .HasColumnType("uuid")
                        .HasColumnName("attendee_profile_id");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uuid")
                        .HasColumnName("session_id");

                    b.Property<bool>("IsCanceled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_canceled");

                    b.HasKey("AttendeeProfileId", "SessionId")
                        .HasName("pk_session_attendances");

                    b.HasIndex("SessionId")
                        .HasDatabaseName("ix_session_attendances_session_id");

                    b.ToTable("session_attendances", "sessions");
                });

            modelBuilder.Entity("Workshopper.Domain.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.Property<string>("SubscriptionType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("subscription_type");

                    b.HasKey("Id")
                        .HasName("pk_subscriptions");

                    b.ToTable("subscriptions", "public");
                });

            modelBuilder.Entity("Workshopper.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("AttendeeProfileId")
                        .HasColumnType("uuid")
                        .HasColumnName("attendee_profile_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("email");

                    b.Property<Guid?>("HostProfileId")
                        .HasColumnType("uuid")
                        .HasColumnName("host_profile_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", "public");
                });

            modelBuilder.Entity("Workshopper.Domain.Users.UserProfiles.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("last_name");

                    b.Property<string>("ProfileType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("profile_type");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_profiles");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_profiles_user_id");

                    b.HasIndex("ProfileType", "UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_profiles_profile_type_user_id");

                    b.ToTable("user_profiles", "public");

                    b.HasDiscriminator<string>("ProfileType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.HybridSession", b =>
                {
                    b.HasBaseType("Workshopper.Domain.Sessions.Session");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("link");

                    b.ToTable("hybrid_sessions", "sessions");
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.OnlineSession", b =>
                {
                    b.HasBaseType("Workshopper.Domain.Sessions.Session");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("link");

                    b.ToTable("online_sessions", "sessions");
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.StationarySession", b =>
                {
                    b.HasBaseType("Workshopper.Domain.Sessions.Session");

                    b.ToTable("stationary_sessions", "sessions");
                });

            modelBuilder.Entity("Workshopper.Domain.Users.UserProfiles.AttendeeProfile", b =>
                {
                    b.HasBaseType("Workshopper.Domain.Users.UserProfiles.UserProfile");

                    b.HasDiscriminator().HasValue("Attendee");
                });

            modelBuilder.Entity("Workshopper.Domain.Users.UserProfiles.HostProfile", b =>
                {
                    b.HasBaseType("Workshopper.Domain.Users.UserProfiles.UserProfile");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("bio");

                    b.Property<string>("Company")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("company");

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_verified");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("title");

                    b.HasDiscriminator().HasValue("Host");
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.Session", b =>
                {
                    b.HasOne("Workshopper.Domain.Users.UserProfiles.HostProfile", "HostProfile")
                        .WithMany("HostedSessions")
                        .HasForeignKey("HostProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_sessions_user_profiles_host_profile_id");

                    b.Navigation("HostProfile");
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.SessionAttendance", b =>
                {
                    b.HasOne("Workshopper.Domain.Users.UserProfiles.AttendeeProfile", "Attendee")
                        .WithMany()
                        .HasForeignKey("AttendeeProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_session_attendances_user_profiles_attendee_profile_id");

                    b.HasOne("Workshopper.Domain.Sessions.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_session_attendances_sessions_session_id");

                    b.Navigation("Attendee");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.HybridSession", b =>
                {
                    b.HasOne("Workshopper.Domain.Sessions.Session", null)
                        .WithOne()
                        .HasForeignKey("Workshopper.Domain.Sessions.HybridSession", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_hybrid_sessions_sessions_id");

                    b.OwnsOne("Workshopper.Domain.Common.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("HybridSessionId")
                                .HasColumnType("uuid")
                                .HasColumnName("hybrid_session_id");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("country");

                            b1.Property<string>("Line1")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("line1");

                            b1.Property<string>("Line2")
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("line2");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("post_code");

                            b1.HasKey("HybridSessionId")
                                .HasName("pk_hybrid_sessions_address");

                            b1.ToTable("hybrid_sessions_address", "sessions");

                            b1.WithOwner()
                                .HasForeignKey("HybridSessionId")
                                .HasConstraintName("fk_hybrid_sessions_address_hybrid_sessions_hybrid_session_id");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.OnlineSession", b =>
                {
                    b.HasOne("Workshopper.Domain.Sessions.Session", null)
                        .WithOne()
                        .HasForeignKey("Workshopper.Domain.Sessions.OnlineSession", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_online_sessions_sessions_id");
                });

            modelBuilder.Entity("Workshopper.Domain.Sessions.StationarySession", b =>
                {
                    b.HasOne("Workshopper.Domain.Sessions.Session", null)
                        .WithOne()
                        .HasForeignKey("Workshopper.Domain.Sessions.StationarySession", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_stationary_sessions_sessions_id");

                    b.OwnsOne("Workshopper.Domain.Common.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("StationarySessionId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("country");

                            b1.Property<string>("Line1")
                                .IsRequired()
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("line1");

                            b1.Property<string>("Line2")
                                .HasMaxLength(250)
                                .HasColumnType("character varying(250)")
                                .HasColumnName("line2");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("post_code");

                            b1.HasKey("StationarySessionId")
                                .HasName("pk_stationary_sessions_address");

                            b1.ToTable("stationary_sessions_address", "sessions");

                            b1.WithOwner()
                                .HasForeignKey("StationarySessionId")
                                .HasConstraintName("fk_stationary_sessions_address_stationary_sessions_id");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Workshopper.Domain.Users.UserProfiles.AttendeeProfile", b =>
                {
                    b.HasOne("Workshopper.Domain.Users.User", "User")
                        .WithOne("AttendeeProfile")
                        .HasForeignKey("Workshopper.Domain.Users.UserProfiles.AttendeeProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_profiles_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Workshopper.Domain.Users.UserProfiles.HostProfile", b =>
                {
                    b.HasOne("Workshopper.Domain.Users.User", "User")
                        .WithOne("HostProfile")
                        .HasForeignKey("Workshopper.Domain.Users.UserProfiles.HostProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_profiles_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Workshopper.Domain.Users.User", b =>
                {
                    b.Navigation("AttendeeProfile");

                    b.Navigation("HostProfile");
                });

            modelBuilder.Entity("Workshopper.Domain.Users.UserProfiles.HostProfile", b =>
                {
                    b.Navigation("HostedSessions");
                });
#pragma warning restore 612, 618
        }
    }
}
