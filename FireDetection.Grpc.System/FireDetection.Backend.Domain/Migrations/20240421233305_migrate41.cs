using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class migrate41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Start",
                table: "AlarmConfigurations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "End",
                table: "AlarmConfigurations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { 0.45m, 0m });

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { 1m, 0.45m });

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 21, 23, 33, 5, 190, DateTimeKind.Utc).AddTicks(9146));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 21, 23, 33, 5, 190, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 21, 23, 33, 5, 190, DateTimeKind.Utc).AddTicks(9162));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Start",
                table: "AlarmConfigurations",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "End",
                table: "AlarmConfigurations",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { 0.45000000000000001, 0.0 });

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { 1.0, 0.45000000000000001 });

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 21, 10, 29, 28, 544, DateTimeKind.Utc).AddTicks(5475));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 21, 10, 29, 28, 544, DateTimeKind.Utc).AddTicks(5478));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 21, 10, 29, 28, 544, DateTimeKind.Utc).AddTicks(5479));
        }
    }
}
