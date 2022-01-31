using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARKServerManager.Migrations
{
    public partial class ServerAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.AddColumn<string>(
                name: "PublicName",
                table: "Server",
                type: "varchar(100)",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Server",
                type: "int",
                nullable: false,
                defaultValue: false);



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.DropTable(
                name: "Server");
        }
    }
}
