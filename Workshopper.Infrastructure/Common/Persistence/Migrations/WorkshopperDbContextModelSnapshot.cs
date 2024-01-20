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
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<DateTimeOffset>("EndDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date_time");

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

                    b.ToTable("sessions", "sessions");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Workshopper.Domain.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("SubscriptionType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("subscription_type");

                    b.Property<Guid>("_adminId")
                        .HasColumnType("uuid")
                        .HasColumnName("admin_id");

                    b.HasKey("Id")
                        .HasName("pk_subscriptions");

                    b.ToTable("subscriptions", "public");
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

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("address");

                    b.ToTable("stationary_sessions", "sessions");
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
                });
#pragma warning restore 612, 618
        }
    }
}
