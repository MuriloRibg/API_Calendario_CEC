using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Calendario_CEC.Migrations
{
    public partial class ColumDeleteAt_DisciplinaTurmaLocalPilar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Turmas",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Pilares",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Locais",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Disciplinas",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Pilares");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Locais");

            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Disciplinas");
        }
    }
}
