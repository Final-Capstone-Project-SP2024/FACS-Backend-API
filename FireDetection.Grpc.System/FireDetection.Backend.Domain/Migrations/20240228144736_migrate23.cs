using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class migrate23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationLog_NotificationType_NotificationTypeId",
                table: "NotificationLog");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationLog_Records_RecordId",
                table: "NotificationLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationLog",
                table: "NotificationLog");

            migrationBuilder.RenameTable(
                name: "NotificationLog",
                newName: "NotificationLogs");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationLog_RecordId",
                table: "NotificationLogs",
                newName: "IX_NotificationLogs_RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationLog_NotificationTypeId",
                table: "NotificationLogs",
                newName: "IX_NotificationLogs_NotificationTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationLogs",
                table: "NotificationLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationLogs_NotificationType_NotificationTypeId",
                table: "NotificationLogs",
                column: "NotificationTypeId",
                principalTable: "NotificationType",
                principalColumn: "NotificationTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationLogs_Records_RecordId",
                table: "NotificationLogs",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationLogs_NotificationType_NotificationTypeId",
                table: "NotificationLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationLogs_Records_RecordId",
                table: "NotificationLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationLogs",
                table: "NotificationLogs");

            migrationBuilder.RenameTable(
                name: "NotificationLogs",
                newName: "NotificationLog");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationLogs_RecordId",
                table: "NotificationLog",
                newName: "IX_NotificationLog_RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationLogs_NotificationTypeId",
                table: "NotificationLog",
                newName: "IX_NotificationLog_NotificationTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationLog",
                table: "NotificationLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationLog_NotificationType_NotificationTypeId",
                table: "NotificationLog",
                column: "NotificationTypeId",
                principalTable: "NotificationType",
                principalColumn: "NotificationTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationLog_Records_RecordId",
                table: "NotificationLog",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
