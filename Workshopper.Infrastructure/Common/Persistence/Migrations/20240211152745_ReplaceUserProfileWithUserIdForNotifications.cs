using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshopper.Infrastructure.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceUserProfileWithUserIdForNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_subscriptions_user_profiles_user_profile_id",
                schema: "notifications",
                table: "notification_subscriptions");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_user_profiles_recepient_id",
                schema: "notifications",
                table: "notifications");

            migrationBuilder.RenameColumn(
                name: "recepient_id",
                schema: "notifications",
                table: "notifications",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_notifications_recepient_id",
                schema: "notifications",
                table: "notifications",
                newName: "ix_notifications_user_id");

            migrationBuilder.RenameColumn(
                name: "user_profile_id",
                schema: "notifications",
                table: "notification_subscriptions",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_notification_subscriptions_user_profile_id",
                schema: "notifications",
                table: "notification_subscriptions",
                newName: "ix_notification_subscriptions_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_notification_subscriptions_users_user_id",
                schema: "notifications",
                table: "notification_subscriptions",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_users_user_id",
                schema: "notifications",
                table: "notifications",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_subscriptions_users_user_id",
                schema: "notifications",
                table: "notification_subscriptions");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_users_user_id",
                schema: "notifications",
                table: "notifications");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "notifications",
                table: "notifications",
                newName: "recepient_id");

            migrationBuilder.RenameIndex(
                name: "ix_notifications_user_id",
                schema: "notifications",
                table: "notifications",
                newName: "ix_notifications_recepient_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "notifications",
                table: "notification_subscriptions",
                newName: "user_profile_id");

            migrationBuilder.RenameIndex(
                name: "ix_notification_subscriptions_user_id",
                schema: "notifications",
                table: "notification_subscriptions",
                newName: "ix_notification_subscriptions_user_profile_id");

            migrationBuilder.AddForeignKey(
                name: "fk_notification_subscriptions_user_profiles_user_profile_id",
                schema: "notifications",
                table: "notification_subscriptions",
                column: "user_profile_id",
                principalSchema: "public",
                principalTable: "user_profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_user_profiles_recepient_id",
                schema: "notifications",
                table: "notifications",
                column: "recepient_id",
                principalSchema: "public",
                principalTable: "user_profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
