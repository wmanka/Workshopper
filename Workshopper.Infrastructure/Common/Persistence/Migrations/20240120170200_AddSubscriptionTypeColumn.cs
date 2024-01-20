using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshopper.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "admin_id",
                schema: "public",
                table: "subscriptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "subscription_type",
                schema: "public",
                table: "subscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "admin_id",
                schema: "public",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "subscription_type",
                schema: "public",
                table: "subscriptions");
        }
    }
}
