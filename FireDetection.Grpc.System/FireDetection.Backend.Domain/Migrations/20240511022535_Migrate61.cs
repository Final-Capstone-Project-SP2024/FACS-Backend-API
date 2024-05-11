using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrate61 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userReponsibilities_Records_RecordId",
                table: "userReponsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_userReponsibilities_Users_UserId",
                table: "userReponsibilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userReponsibilities",
                table: "userReponsibilities");

            migrationBuilder.RenameTable(
                name: "userReponsibilities",
                newName: "UserReponsibilities");

            migrationBuilder.RenameIndex(
                name: "IX_userReponsibilities_UserId",
                table: "UserReponsibilities",
                newName: "IX_UserReponsibilities_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_userReponsibilities_RecordId",
                table: "UserReponsibilities",
                newName: "IX_UserReponsibilities_RecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReponsibilities",
                table: "UserReponsibilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReponsibilities_Records_RecordId",
                table: "UserReponsibilities",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReponsibilities_Users_UserId",
                table: "UserReponsibilities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReponsibilities_Records_RecordId",
                table: "UserReponsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReponsibilities_Users_UserId",
                table: "UserReponsibilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReponsibilities",
                table: "UserReponsibilities");

            migrationBuilder.RenameTable(
                name: "UserReponsibilities",
                newName: "userReponsibilities");

            migrationBuilder.RenameIndex(
                name: "IX_UserReponsibilities_UserId",
                table: "userReponsibilities",
                newName: "IX_userReponsibilities_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReponsibilities_RecordId",
                table: "userReponsibilities",
                newName: "IX_userReponsibilities_RecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userReponsibilities",
                table: "userReponsibilities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userReponsibilities_Records_RecordId",
                table: "userReponsibilities",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userReponsibilities_Users_UserId",
                table: "userReponsibilities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
