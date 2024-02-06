using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshopper.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SetupNotificationsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "notifications");

            migrationBuilder.CreateTable(
                name: "notification_subscriptions",
                schema: "notifications",
                columns: table => new
                {
                    notification_type = table.Column<string>(type: "text", nullable: false),
                    notification_delivery_type = table.Column<string>(type: "text", nullable: false),
                    user_profile_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification_subscriptions", x => new { x.notification_type, x.notification_delivery_type, x.user_profile_id });
                    table.ForeignKey(
                        name: "fk_notification_subscriptions_user_profiles_user_profile_id",
                        column: x => x.user_profile_id,
                        principalSchema: "public",
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                schema: "notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    notification_type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", maxLength: 1000, nullable: false),
                    recepient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_user_profiles_recepient_id",
                        column: x => x.recepient_id,
                        principalSchema: "public",
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_notification_subscriptions_notification_type_notification_d",
                schema: "notifications",
                table: "notification_subscriptions",
                columns: new[] { "notification_type", "notification_delivery_type", "user_profile_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_notification_subscriptions_user_profile_id",
                schema: "notifications",
                table: "notification_subscriptions",
                column: "user_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_recepient_id",
                schema: "notifications",
                table: "notifications",
                column: "recepient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_subscriptions",
                schema: "notifications");

            migrationBuilder.DropTable(
                name: "notifications",
                schema: "notifications");
        }
    }
}
