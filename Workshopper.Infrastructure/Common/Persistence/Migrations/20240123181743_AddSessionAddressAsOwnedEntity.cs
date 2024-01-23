using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshopper.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionAddressAsOwnedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                schema: "sessions",
                table: "stationary_sessions");

            migrationBuilder.DropColumn(
                name: "address",
                schema: "sessions",
                table: "hybrid_sessions");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hybrid_sessions_address",
                schema: "sessions");

            migrationBuilder.DropTable(
                name: "stationary_sessions_address",
                schema: "sessions");

            migrationBuilder.AddColumn<string>(
                name: "address",
                schema: "sessions",
                table: "stationary_sessions",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address",
                schema: "sessions",
                table: "hybrid_sessions",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
