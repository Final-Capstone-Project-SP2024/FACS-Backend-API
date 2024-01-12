using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class second_initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlCameras_Cameras_CameraId",
                table: "ControlCameras");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_UserId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Percent",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cameras");

            migrationBuilder.RenameColumn(
                name: "CameraId",
                table: "ControlCameras",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "CameraID",
                table: "ControlCameras",
                newName: "LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_ControlCameras_CameraId",
                table: "ControlCameras",
                newName: "IX_ControlCameras_LocationId");

            migrationBuilder.AddColumn<decimal>(
                name: "PredictedPercent",
                table: "Records",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UserRatingPercent",
                table: "Records",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationID",
                table: "Cameras",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Cameras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ActionType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionName = table.Column<string>(type: "text", nullable: false),
                    ActionDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    LevelID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.LevelID);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationName = table.Column<string>(type: "text", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordProcess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<int>(type: "integer", nullable: false),
                    RecordID = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecordProcess_ActionType_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordProcess_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecordProcess_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<int>(type: "integer", nullable: false),
                    RecordID = table.Column<Guid>(type: "uuid", nullable: false),
                    LevelID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmRate_Level_LevelID",
                        column: x => x.LevelID,
                        principalTable: "Level",
                        principalColumn: "LevelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmRate_Records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlarmRate_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_LocationId",
                table: "Cameras",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRate_LevelID",
                table: "AlarmRate",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRate_RecordId",
                table: "AlarmRate",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmRate_UserId",
                table: "AlarmRate",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordProcess_ActionTypeId",
                table: "RecordProcess",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordProcess_RecordId",
                table: "RecordProcess",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RecordProcess_UserId",
                table: "RecordProcess",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_Location_LocationId",
                table: "Cameras",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ControlCameras_Location_LocationId",
                table: "ControlCameras",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_Location_LocationId",
                table: "Cameras");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlCameras_Location_LocationId",
                table: "ControlCameras");

            migrationBuilder.DropTable(
                name: "AlarmRate");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "RecordProcess");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "ActionType");

            migrationBuilder.DropIndex(
                name: "IX_Cameras_LocationId",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "PredictedPercent",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "UserRatingPercent",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Cameras");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "ControlCameras",
                newName: "CameraId");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "ControlCameras",
                newName: "CameraID");

            migrationBuilder.RenameIndex(
                name: "IX_ControlCameras_LocationId",
                table: "ControlCameras",
                newName: "IX_ControlCameras_CameraId");

            migrationBuilder.AddColumn<int>(
                name: "Percent",
                table: "Records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "Records",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Cameras",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Records_UserId",
                table: "Records",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlCameras_Cameras_CameraId",
                table: "ControlCameras",
                column: "CameraId",
                principalTable: "Cameras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Users_UserId",
                table: "Records",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
