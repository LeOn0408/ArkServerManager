using Microsoft.EntityFrameworkCore.Migrations;

namespace ArkWeb.Migrations
{
    public partial class ApiSettingsOnlinePlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "APISetting",
                columns: new[] { "ID", "ApiHost", "ApiName" },
                values: new object[] { 1, 0, "OnlinePlayer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
