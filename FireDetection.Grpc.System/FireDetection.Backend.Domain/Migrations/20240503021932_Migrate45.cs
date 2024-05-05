using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrate45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 15, 10, 848, DateTimeKind.Utc).AddTicks(2453));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 15, 10, 848, DateTimeKind.Utc).AddTicks(2468));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 2, 15, 10, 848, DateTimeKind.Utc).AddTicks(2473));
        }
    }
}
