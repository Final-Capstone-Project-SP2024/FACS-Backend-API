using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrate51 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmRates");

            migrationBuilder.DropTable(
                name: "Levels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.LevelID);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LevelID = table.Column<int>(type: "integer", nullable: false),
                    RecordID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmRates_Levels_LevelID",
                        column: x => x.LevelID,
                        principalTable: "Levels",
                        principalColumn: "LevelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmRates_Records_RecordID",
                        column: x => x.RecordID,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmRates_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "LevelID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Small Fire", "Level 1" },
                    { 2, "Fire ", "Level 2" },
                    { 3, "Fire ", "Level 3" },
                    { 4, "Fire ", "Level 4" },
                    { 5, "Fire ", "Level 5" },
                    { 6, "Fake Alarm", "Fake Alarm" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRates_LevelID",
                table: "AlarmRates",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRates_RecordID",
                table: "AlarmRates",
                column: "RecordID");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRates_UserID",
                table: "AlarmRates",
                column: "UserID");
        }
    }
}
