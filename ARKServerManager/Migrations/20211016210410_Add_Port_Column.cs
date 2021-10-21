using Microsoft.EntityFrameworkCore.Migrations;

namespace ARKServerManager.Migrations
{
    public partial class Add_Port_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Port",
                table: "Server",
                newName: "RemotePort");

            migrationBuilder.AddColumn<int>(
                name: "GamePort",
                table: "Server",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalPort",
                table: "Server",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamePort",
                table: "Server");

            migrationBuilder.DropColumn(
                name: "LocalPort",
                table: "Server");

            migrationBuilder.RenameColumn(
                name: "RemotePort",
                table: "Server",
                newName: "Port");
        }
    }
}
