using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrate50 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugsReports");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ActionPlanType");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "ManualPlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionPlanType",
                columns: table => new
                {
                    ActionPlanTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionPlanTypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPlanType", x => x.ActionPlanTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BugsReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    BugDescription = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Feature = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsSolved = table.Column<bool>(type: "boolean", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugsReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BugsReports_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Context = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManualPlans",
                columns: table => new
                {
                    ManualPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CameraLimited = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LocationLimited = table.Column<int>(type: "integer", nullable: false),
                    ManualPlanName = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    UserLimited = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualPlans", x => x.ManualPlanId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ManualPlanID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractImage = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    isPaid = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_ManualPlans_ManualPlanID",
                        column: x => x.ManualPlanID,
                        principalTable: "ManualPlans",
                        principalColumn: "ManualPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActionPlanTypeID = table.Column<int>(type: "integer", nullable: false),
                    ContractID = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentTypeID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    isPaid = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_ActionPlanType_ActionPlanTypeID",
                        column: x => x.ActionPlanTypeID,
                        principalTable: "ActionPlanType",
                        principalColumn: "ActionPlanTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Contracts_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentType_PaymentTypeID",
                        column: x => x.PaymentTypeID,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActionPlanType",
                columns: new[] { "ActionPlanTypeId", "ActionPlanTypeName" },
                values: new object[,]
                {
                    { 1, "Upgrade" },
                    { 2, "Downgrade" },
                    { 3, "Renewal" }
                });

            migrationBuilder.InsertData(
                table: "ManualPlans",
                columns: new[] { "ManualPlanId", "CameraLimited", "CreatedBy", "CreatedDate", "DeleteBy", "DeletedDate", "IsDeleted", "LastModified", "LocationLimited", "ManualPlanName", "ModifiedBy", "Price", "UserLimited" },
                values: new object[,]
                {
                    { 1, 4, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 5, 4, 7, 25, 40, 291, DateTimeKind.Utc).AddTicks(864), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Basic", new Guid("00000000-0000-0000-0000-000000000000"), 1000m, 5 },
                    { 2, 8, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 5, 4, 7, 25, 40, 291, DateTimeKind.Utc).AddTicks(868), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Standard", new Guid("00000000-0000-0000-0000-000000000000"), 2000m, 10 },
                    { 3, 20, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 5, 4, 7, 25, 40, 291, DateTimeKind.Utc).AddTicks(869), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Premium", new Guid("00000000-0000-0000-0000-000000000000"), 3000m, 20 }
                });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: new[] { "PaymentTypeId", "PaymentTypeName" },
                values: new object[,]
                {
                    { 1, "DirectPayment" },
                    { 2, "OnlinePayment" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BugsReports_UserID",
                table: "BugsReports",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ManualPlanID",
                table: "Contracts",
                column: "ManualPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserID",
                table: "Contracts",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ActionPlanTypeID",
                table: "Transactions",
                column: "ActionPlanTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ContractID",
                table: "Transactions",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentTypeID",
                table: "Transactions",
                column: "PaymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions",
                column: "UserID");
        }
    }
}
