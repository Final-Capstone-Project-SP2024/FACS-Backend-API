using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class HotFix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 7, 51, 7, 274, DateTimeKind.Utc).AddTicks(4810));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 7, 51, 7, 274, DateTimeKind.Utc).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 7, 51, 7, 274, DateTimeKind.Utc).AddTicks(4815));

            migrationBuilder.InsertData(
                table: "RecordTypes",
                columns: new[] { "RecordTypeId", "Name" },
                values: new object[] { 3, "AlarmByUser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecordTypes",
                keyColumn: "RecordTypeId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 19, 15, 26, 57, 882, DateTimeKind.Utc).AddTicks(9810));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 19, 15, 26, 57, 882, DateTimeKind.Utc).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 19, 15, 26, 57, 882, DateTimeKind.Utc).AddTicks(9815));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 3, "Admin" });
        }
    }
}
