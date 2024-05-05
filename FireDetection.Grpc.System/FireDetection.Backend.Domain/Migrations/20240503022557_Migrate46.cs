using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrate46 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReponsibility_Records_RecordId",
                table: "UserReponsibility");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReponsibility_Users_UserId",
                table: "UserReponsibility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReponsibility",
                table: "UserReponsibility");

            migrationBuilder.RenameTable(
                name: "UserReponsibility",
                newName: "userReponsibilities");

            migrationBuilder.RenameIndex(
                name: "IX_UserReponsibility_UserId",
                table: "userReponsibilities",
                newName: "IX_userReponsibilities_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReponsibility_RecordId",
                table: "userReponsibilities",
                newName: "IX_userReponsibilities_RecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userReponsibilities",
                table: "userReponsibilities",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 25, 57, 42, DateTimeKind.Utc).AddTicks(6692));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 25, 57, 42, DateTimeKind.Utc).AddTicks(6699));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 25, 57, 42, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.AddForeignKey(
                name: "FK_userReponsibilities_Records_RecordId",
                table: "userReponsibilities",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userReponsibilities_Users_UserId",
                table: "userReponsibilities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userReponsibilities_Records_RecordId",
                table: "userReponsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_userReponsibilities_Users_UserId",
                table: "userReponsibilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userReponsibilities",
                table: "userReponsibilities");

            migrationBuilder.RenameTable(
                name: "userReponsibilities",
                newName: "UserReponsibility");

            migrationBuilder.RenameIndex(
                name: "IX_userReponsibilities_UserId",
                table: "UserReponsibility",
                newName: "IX_UserReponsibility_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_userReponsibilities_RecordId",
                table: "UserReponsibility",
                newName: "IX_UserReponsibility_RecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReponsibility",
                table: "UserReponsibility",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 19, 32, 1, DateTimeKind.Utc).AddTicks(9723));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 19, 32, 1, DateTimeKind.Utc).AddTicks(9726));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 19, 32, 1, DateTimeKind.Utc).AddTicks(9728));

            migrationBuilder.AddForeignKey(
                name: "FK_UserReponsibility_Records_RecordId",
                table: "UserReponsibility",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReponsibility_Users_UserId",
                table: "UserReponsibility",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
