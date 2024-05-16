using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireDetection.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 1,
                column: "NumberOfNotification",
                value: null);

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 2,
                columns: new[] { "AlarmNameConfiguration", "End", "NumberOfNotification" },
                values: new object[] { "Fire", 10m, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 1,
                column: "NumberOfNotification",
                value: 0);

            migrationBuilder.UpdateData(
                table: "AlarmConfigurations",
                keyColumn: "AlarmConfigurationId",
                keyValue: 2,
                columns: new[] { "AlarmNameConfiguration", "End", "NumberOfNotification" },
                values: new object[] { "Small Fire", 60m, 1 });

            migrationBuilder.InsertData(
                table: "AlarmConfigurations",
                columns: new[] { "AlarmConfigurationId", "AlarmNameConfiguration", "End", "NumberOfNotification", "Start" },
                values: new object[] { 3, "Big Fire", 40m, 99, 60m });
        }
    }
}
