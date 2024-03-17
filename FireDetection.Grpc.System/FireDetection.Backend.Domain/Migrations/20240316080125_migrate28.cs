using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class migrate28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ActionPlanType_UserPlanTypeID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserTransactions_UserTransactionID",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "UserTransactions");

            migrationBuilder.RenameColumn(
                name: "UserTransactionID",
                table: "Transactions",
                newName: "ContractID");

            migrationBuilder.RenameColumn(
                name: "UserPlanTypeID",
                table: "Transactions",
                newName: "ActionPlanTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserTransactionID",
                table: "Transactions",
                newName: "IX_Transactions_ContractID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserPlanTypeID",
                table: "Transactions",
                newName: "IX_Transactions_ActionPlanTypeID");

            migrationBuilder.RenameColumn(
                name: "ManualPlanNameId",
                table: "ManualPlans",
                newName: "ManualPlanId");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Feedbacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Feedbacks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
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

            migrationBuilder.InsertData(
                table: "ActionPlanType",
                columns: new[] { "ActionPlanTypeId", "ActionPlanTypeName" },
                values: new object[] { 3, "Renewal" });

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 16, 8, 1, 25, 152, DateTimeKind.Utc).AddTicks(1641));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 16, 8, 1, 25, 152, DateTimeKind.Utc).AddTicks(1643));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 16, 8, 1, 25, 152, DateTimeKind.Utc).AddTicks(1645));

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ManualPlanID",
                table: "Contracts",
                column: "ManualPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserID",
                table: "Contracts",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Users_UserId",
                table: "Feedbacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ActionPlanType_ActionPlanTypeID",
                table: "Transactions",
                column: "ActionPlanTypeID",
                principalTable: "ActionPlanType",
                principalColumn: "ActionPlanTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Contracts_ContractID",
                table: "Transactions",
                column: "ContractID",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_UserId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ActionPlanType_ActionPlanTypeID",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Contracts_ContractID",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks");

            migrationBuilder.DeleteData(
                table: "ActionPlanType",
                keyColumn: "ActionPlanTypeId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Feedbacks");

            migrationBuilder.RenameColumn(
                name: "ContractID",
                table: "Transactions",
                newName: "UserTransactionID");

            migrationBuilder.RenameColumn(
                name: "ActionPlanTypeID",
                table: "Transactions",
                newName: "UserPlanTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ContractID",
                table: "Transactions",
                newName: "IX_Transactions_UserTransactionID");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ActionPlanTypeID",
                table: "Transactions",
                newName: "IX_Transactions_UserPlanTypeID");

            migrationBuilder.RenameColumn(
                name: "ManualPlanId",
                table: "ManualPlans",
                newName: "ManualPlanNameId");

            migrationBuilder.CreateTable(
                name: "UserTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ManualPlanID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractImage = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeleteBy = table.Column<Guid>(type: "uuid", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    isPaid = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanNameId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 15, 15, 54, 39, 803, DateTimeKind.Utc).AddTicks(4157));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanNameId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 15, 15, 54, 39, 803, DateTimeKind.Utc).AddTicks(4161));

            migrationBuilder.UpdateData(
                table: "ManualPlans",
                keyColumn: "ManualPlanNameId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 15, 15, 54, 39, 803, DateTimeKind.Utc).AddTicks(4162));

            migrationBuilder.CreateIndex(
                name: "IX_UserTransactions_ManualPlanID",
                table: "UserTransactions",
                column: "ManualPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTransactions_UserID",
                table: "UserTransactions",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ActionPlanType_UserPlanTypeID",
                table: "Transactions",
                column: "UserPlanTypeID",
                principalTable: "ActionPlanType",
                principalColumn: "ActionPlanTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserTransactions_UserTransactionID",
                table: "Transactions",
                column: "UserTransactionID",
                principalTable: "UserTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
