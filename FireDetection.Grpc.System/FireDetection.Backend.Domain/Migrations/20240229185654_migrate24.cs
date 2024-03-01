using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class migrate24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 4,
                column: "ActionName",
                value: "Alarm Level 4");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "ActionDescription", "ActionName" },
                values: new object[] { "a large fire can affect and cause damage, mobilizing everyone", "Alarm Level 5" });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "ActionDescription", "ActionName" },
                values: new object[] { "a large fire can affect and cause damage, mobilizing everyone", "End Action" });

            migrationBuilder.InsertData(
                table: "ActionTypes",
                columns: new[] { "ID", "ActionDescription", "ActionName" },
                values: new object[,]
                {
                    { 7, "", "Fake  Alarm" },
                    { 8, "AI model is disconnected from the camera", "Repair the camera" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 4,
                column: "ActionName",
                value: "End Action");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "ActionDescription", "ActionName" },
                values: new object[] { "", "Fake  Alarm" });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "ActionDescription", "ActionName" },
                values: new object[] { "AI model is disconnected from the camera", "Repair the camera" });
        }
    }
}
