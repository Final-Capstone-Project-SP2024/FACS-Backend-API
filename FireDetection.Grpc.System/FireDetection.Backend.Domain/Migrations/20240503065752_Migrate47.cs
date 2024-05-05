using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrate47 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 6, 57, 52, 26, DateTimeKind.Utc).AddTicks(6047));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 6, 57, 52, 26, DateTimeKind.Utc).AddTicks(6062));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 3, 6, 57, 52, 26, DateTimeKind.Utc).AddTicks(6068));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
