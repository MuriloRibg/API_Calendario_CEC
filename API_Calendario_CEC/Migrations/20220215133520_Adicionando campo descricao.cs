using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Calendario_CEC.Migrations
{
    public partial class Adicionandocampodescricao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Eventos",
                type: "text",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Eventos");
        }
    }
}
