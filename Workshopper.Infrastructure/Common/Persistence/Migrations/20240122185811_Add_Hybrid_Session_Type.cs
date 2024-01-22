using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshopper.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Hybrid_Session_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "sessions",
                table: "sessions",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "hybrid_sessions",
                schema: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    link = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hybrid_sessions",
                schema: "sessions");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "sessions",
                table: "sessions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
