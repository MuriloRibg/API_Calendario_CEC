using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Calendario_CEC.Migrations
{
    public partial class ColumDeleteAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Instrutores",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Instrutores");
        }
    }
}
