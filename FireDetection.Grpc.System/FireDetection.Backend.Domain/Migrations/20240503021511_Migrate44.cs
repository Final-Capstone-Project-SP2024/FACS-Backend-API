using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrate44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserReponsibility",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReponsibility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReponsibility_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReponsibility_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserReponsibility_RecordId",
                table: "UserReponsibility",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReponsibility_UserId",
                table: "UserReponsibility",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReponsibility");

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 22, 7, 43, 16, 98, DateTimeKind.Utc).AddTicks(794));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 22, 7, 43, 16, 98, DateTimeKind.Utc).AddTicks(798));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 4, 22, 7, 43, 16, 98, DateTimeKind.Utc).AddTicks(800));
        }
    }
}
