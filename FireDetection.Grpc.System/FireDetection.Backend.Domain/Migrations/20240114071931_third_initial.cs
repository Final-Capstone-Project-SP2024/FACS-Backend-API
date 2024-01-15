using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class third_initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ActionType",
                columns: new[] { "ID", "ActionDescription", "ActionName" },
                values: new object[,]
                {
                    { 1, "actiondes", "action" },
                    { 2, "actiondes", "action" },
                    { 3, "actiondes", "action" }
                });

            migrationBuilder.InsertData(
                table: "Level",
                columns: new[] { "LevelID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Small Fire", "Level 1" },
                    { 2, "Fire ", "Level 2" },
                    { 3, "Fire ", "Level 3" },
                    { 4, "Fire ", "Level 4" },
                    { 5, "Fire ", "Level 5" }
                });

            migrationBuilder.InsertData(
                table: "MediaTypes",
                columns: new[] { "MediaTypeID", "MediaName" },
                values: new object[,]
                {
                    { 1, "video" },
                    { 2, "image" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Manager" },
                    { 2, "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActionType",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActionType",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActionType",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Level",
                keyColumn: "LevelID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "MediaTypeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "MediaTypeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);
        }
    }
}
