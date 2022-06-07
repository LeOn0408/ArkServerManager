using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARKServerManager.Migrations
{
    public partial class TypeServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeServer",
                table: "Server",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeServer",
                table: "Server");
        }
    }
}
