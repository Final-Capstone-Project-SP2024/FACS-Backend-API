using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrate48 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Transactions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "NotificationLogs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ManualPlans",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Locations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Contracts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Cameras",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "BugsReports",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AlarmNameConfiguration",
                table: "AlarmConfigurations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 1,
                columns: new[] { "AlarmNameConfiguration", "End", "NumberOfNotification" },
                values: new object[] { "Fake Alarm", 20m, 0 });

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 2,
                columns: new[] { "AlarmNameConfiguration", "End", "NumberOfNotification", "Start" },
                values: new object[] { "Small Fire", 60m, 1, 20m });

            migrationBuilder.InsertData(
                table: "AlarmConfigurations",
                columns: new[] { "AlarmConfigurationId", "AlarmNameConfiguration", "End", "NumberOfNotification", "Start" },
                values: new object[] { 3, "Big Fire", 40m, 99, 60m });

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DeletedDate" },
                values: new object[] { new DateTime(2024, 5, 4, 7, 25, 40, 291, DateTimeKind.Utc).AddTicks(864), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DeletedDate" },
                values: new object[] { new DateTime(2024, 5, 4, 7, 25, 40, 291, DateTimeKind.Utc).AddTicks(868), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "DeletedDate" },
                values: new object[] { new DateTime(2024, 5, 4, 7, 25, 40, 291, DateTimeKind.Utc).AddTicks(869), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"),
                column: "DeletedDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "NotificationLogs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ManualPlans");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "BugsReports");

            migrationBuilder.DropColumn(
                name: "AlarmNameConfiguration",
                table: "AlarmConfigurations");

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 1,
                columns: new[] { "End", "NumberOfNotification" },
                values: new object[] { 0.45m, 1 });

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 2,
                columns: new[] { "End", "NumberOfNotification", "Start" },
                values: new object[] { 1m, 99, 0.45m });

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
    }
}
