using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class migrate27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    Feature = table.Column<string>(type: "text", nullable: false),
                    BugDescription = table.Column<string>(type: "text", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Context = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManualPlans",
                columns: table => new
                {
                    ManualPlanNameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ManualPlanName = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    LocationLimited = table.Column<int>(type: "integer", nullable: false),
                    CameraLimited = table.Column<int>(type: "integer", nullable: false),
                    UserLimited = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualPlans", x => x.ManualPlanNameId);
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
                name: "UserTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isPaid = table.Column<bool>(type: "boolean", nullable: false),
                    ContractImage = table.Column<string>(type: "text", nullable: false),
                    ManualPlanID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTransactions_ManualPlans_ManualPlanID",
                        column: x => x.ManualPlanID,
                        principalTable: "ManualPlans",
                        principalColumn: "ManualPlanNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTransactions_Users_UserID",
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
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    isPaid = table.Column<bool>(type: "boolean", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserTransactionID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserPlanTypeID = table.Column<int>(type: "integer", nullable: false),
                    PaymentTypeID = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_ActionPlanType_UserPlanTypeID",
                        column: x => x.UserPlanTypeID,
                        principalTable: "ActionPlanType",
                        principalColumn: "ActionPlanTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentType_PaymentTypeID",
                        column: x => x.PaymentTypeID,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_UserTransactions_UserTransactionID",
                        column: x => x.UserTransactionID,
                        principalTable: "UserTransactions",
                        principalColumn: "Id",
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
                    { 2, "Downgrade" }
                });

            migrationBuilder.InsertData(
                table: "ManualPlans",
                columns: new[] { "ManualPlanNameId", "CameraLimited", "CreatedBy", "CreatedDate", "DeleteBy", "IsDeleted", "LastModified", "LocationLimited", "ManualPlanName", "ModifiedBy", "Price", "UserLimited" },
                values: new object[,]
                {
                    { 1, 4, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 3, 15, 15, 54, 39, 803, DateTimeKind.Utc).AddTicks(4157), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Basic", new Guid("00000000-0000-0000-0000-000000000000"), 1000m, 5 },
                    { 2, 8, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 3, 15, 15, 54, 39, 803, DateTimeKind.Utc).AddTicks(4161), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Standard", new Guid("00000000-0000-0000-0000-000000000000"), 2000m, 10 },
                    { 3, 20, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 3, 15, 15, 54, 39, 803, DateTimeKind.Utc).AddTicks(4162), null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Premium", new Guid("00000000-0000-0000-0000-000000000000"), 3000m, 20 }
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
                name: "IX_Transactions_PaymentTypeID",
                table: "Transactions",
                column: "PaymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserPlanTypeID",
                table: "Transactions",
                column: "UserPlanTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserTransactionID",
                table: "Transactions",
                column: "UserTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTransactions_ManualPlanID",
                table: "UserTransactions",
                column: "ManualPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTransactions_UserID",
                table: "UserTransactions",
                column: "UserID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "UserTransactions");

            migrationBuilder.DropTable(
                name: "ManualPlans");
        }
    }
}
