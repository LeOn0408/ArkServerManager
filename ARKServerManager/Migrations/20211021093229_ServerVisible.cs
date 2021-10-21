using Microsoft.EntityFrameworkCore.Migrations;

namespace ARKServerManager.Migrations
{
    public partial class ServerVisible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Visible",
                table: "Server",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Server");
        }
    }
}
