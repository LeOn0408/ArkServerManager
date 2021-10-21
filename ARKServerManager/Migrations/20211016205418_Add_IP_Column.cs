using Microsoft.EntityFrameworkCore.Migrations;

namespace ARKServerManager.Migrations
{
    public partial class Add_IP_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IP",
                table: "Server",
                newName: "RemoteIP");

            migrationBuilder.AddColumn<string>(
                name: "LocalIP",
                table: "Server",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalIP",
                table: "Server");

            migrationBuilder.RenameColumn(
                name: "RemoteIP",
                table: "Server",
                newName: "IP");
        }
    }
}
