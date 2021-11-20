using Microsoft.EntityFrameworkCore.Migrations;

namespace ArkWeb.Migrations
{
    public partial class AddFeedbackTableColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Resolved",
                table: "ArkFeedback",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resolved",
                table: "ArkFeedback");
        }
    }
}
