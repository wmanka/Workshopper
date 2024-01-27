using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshopper.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sessions");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "subscriptions",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    subscription_type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscriptions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    password = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    host_profile_id = table.Column<Guid>(type: "uuid", nullable: true),
                    attendee_profile_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_profiles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    profile_type = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    last_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    company = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    bio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_verified = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_profiles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                schema: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_type = table.Column<string>(type: "text", nullable: false),
                    session_type = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    start_date_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_date_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    places = table.Column<int>(type: "integer", nullable: false),
                    is_canceled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    host_profile_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_sessions_user_profiles_host_profile_id",
                        column: x => x.host_profile_id,
                        principalSchema: "public",
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hybrid_sessions",
                schema: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    link = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hybrid_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_hybrid_sessions_sessions_id",
                        column: x => x.id,
                        principalSchema: "sessions",
                        principalTable: "sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "online_sessions",
                schema: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    link = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_online_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_online_sessions_sessions_id",
                        column: x => x.id,
                        principalSchema: "sessions",
                        principalTable: "sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session_attendances",
                schema: "sessions",
                columns: table => new
                {
                    session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    attendee_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_canceled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_session_attendances", x => new { x.attendee_profile_id, x.session_id });
                    table.ForeignKey(
                        name: "fk_session_attendances_sessions_session_id",
                        column: x => x.session_id,
                        principalSchema: "sessions",
                        principalTable: "sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_session_attendances_user_profiles_attendee_profile_id",
                        column: x => x.attendee_profile_id,
                        principalSchema: "public",
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stationary_sessions",
                schema: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stationary_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_stationary_sessions_sessions_id",
                        column: x => x.id,
                        principalSchema: "sessions",
                        principalTable: "sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hybrid_sessions_address",
                schema: "sessions",
                columns: table => new
                {
                    hybrid_session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    line1 = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    line2 = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    city = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    country = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    post_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hybrid_sessions_address", x => x.hybrid_session_id);
                    table.ForeignKey(
                        name: "fk_hybrid_sessions_address_hybrid_sessions_hybrid_session_id",
                        column: x => x.hybrid_session_id,
                        principalSchema: "sessions",
                        principalTable: "hybrid_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stationary_sessions_address",
                schema: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    line1 = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    line2 = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    city = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    country = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    post_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stationary_sessions_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_stationary_sessions_address_stationary_sessions_id",
                        column: x => x.id,
                        principalSchema: "sessions",
                        principalTable: "stationary_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_session_attendances_session_id",
                schema: "sessions",
                table: "session_attendances",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "ix_sessions_host_profile_id",
                schema: "sessions",
                table: "sessions",
                column: "host_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_profiles_profile_type_user_id",
                schema: "public",
                table: "user_profiles",
                columns: new[] { "profile_type", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_profiles_user_id",
                schema: "public",
                table: "user_profiles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hybrid_sessions_address",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "online_sessions",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "session_attendances",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "stationary_sessions_address",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "subscriptions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "hybrid_sessions",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "stationary_sessions",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "sessions",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "user_profiles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");
        }
    }
}
