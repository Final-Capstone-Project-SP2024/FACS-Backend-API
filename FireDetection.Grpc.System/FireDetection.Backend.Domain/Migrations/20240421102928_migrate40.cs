using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class migrate40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRatingPercent",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "AlarmConfigurationId",
                table: "Records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishAlarm",
                table: "Records",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RecommendAlarmLevel",
                table: "Records",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationImage",
                table: "Locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CameraImage",
                table: "Cameras",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AlarmConfigurations",
                columns: table => new
                {
                    AlarmConfigurationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Start = table.Column<double>(type: "double precision", nullable: true),
                    End = table.Column<double>(type: "double precision", nullable: true),
                    NumberOfNotification = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmConfigurations", x => x.AlarmConfigurationId);
                });

            migrationBuilder.InsertData(
                table: "AlarmConfigurations",
                columns: new[] { "AlarmConfigurationId", "End", "NumberOfNotification", "Start" },
                values: new object[,]
                {
                    { 1, 0.45000000000000001, 1, 0.0 },
                    { 2, 1.0, 99, 0.45000000000000001 }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Records_AlarmConfigurationId",
                table: "Records",
                column: "AlarmConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AlarmConfigurations_AlarmConfigurationId",
                table: "Records",
                column: "AlarmConfigurationId",
                principalTable: "AlarmConfigurations",
                principalColumn: "AlarmConfigurationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_AlarmConfigurations_AlarmConfigurationId",
                table: "Records");

            migrationBuilder.DropTable(
                name: "AlarmConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_Records_AlarmConfigurationId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "AlarmConfigurationId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "FinishAlarm",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "RecommendAlarmLevel",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "LocationImage",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CameraImage",
                table: "Cameras");

            migrationBuilder.AddColumn<decimal>(
                name: "UserRatingPercent",
                table: "Records",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 14, 37, 29, 181, DateTimeKind.Utc).AddTicks(3986));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 14, 37, 29, 181, DateTimeKind.Utc).AddTicks(3989));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 11, 14, 37, 29, 181, DateTimeKind.Utc).AddTicks(3991));
        }
    }
}
