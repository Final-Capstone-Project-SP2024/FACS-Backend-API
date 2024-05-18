using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ConfigAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Max",
                table: "ActionTypes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Min",
                table: "ActionTypes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Max", "Min" },
                values: new object[] { 60m, 0m });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Max", "Min" },
                values: new object[] { 70m, 60m });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Max", "Min" },
                values: new object[] { 80m, 70m });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Max", "Min" },
                values: new object[] { 90m, 80m });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "Max", "Min" },
                values: new object[] { 100m, 90m });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "Max", "Min" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "Max", "Min" },
                values: new object[] { 0m, 0m });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "Max", "Min" },
                values: new object[] { 0m, 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Max",
                table: "ActionTypes");

            migrationBuilder.DropColumn(
                name: "Min",
                table: "ActionTypes");
        }
    }
}
