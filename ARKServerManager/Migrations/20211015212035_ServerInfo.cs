using Microsoft.EntityFrameworkCore.Migrations;

namespace ARKServerManager.Migrations
{
    public partial class ServerInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Map",
                table: "Server",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "Server",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Server",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Map",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Server");
        }
    }
}
