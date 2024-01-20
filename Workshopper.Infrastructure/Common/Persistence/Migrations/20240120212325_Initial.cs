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
                name: "sessions",
                schema: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_type = table.Column<string>(type: "text", nullable: false),
                    session_type = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    start_date_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_date_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    places = table.Column<int>(type: "integer", nullable: false),
                    is_canceled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    subscription_type = table.Column<string>(type: "text", nullable: false),
                    admin_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscriptions", x => x.id);
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
                name: "stationary_sessions",
                schema: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "online_sessions",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "stationary_sessions",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "subscriptions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "sessions",
                schema: "sessions");
        }
    }
}
