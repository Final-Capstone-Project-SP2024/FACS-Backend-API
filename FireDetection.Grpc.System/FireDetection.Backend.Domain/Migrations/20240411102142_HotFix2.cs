using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class HotFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 10, 21, 42, 583, DateTimeKind.Utc).AddTicks(3629));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 10, 21, 42, 583, DateTimeKind.Utc).AddTicks(3634));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 10, 21, 42, 583, DateTimeKind.Utc).AddTicks(3715));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"),
                column: "Password",
                value: "NJt3DCzVWSRDN7SigMcj+v/M8v+OWeZPBW/lApGrc+thCg3X");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"),
                column: "Password",
                value: "12345");
        }
    }
}
