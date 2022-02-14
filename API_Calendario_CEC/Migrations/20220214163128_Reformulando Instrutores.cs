using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Calendario_CEC.Migrations
{
    public partial class ReformulandoInstrutores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instrutores_Pilares_Id_Pilar",
                table: "Instrutores");

            migrationBuilder.DropIndex(
                name: "IX_Instrutores_Id_Pilar",
                table: "Instrutores");

            migrationBuilder.DropColumn(
                name: "Id_Pilar",
                table: "Instrutores");

            migrationBuilder.AddColumn<string>(
                name: "Pilar",
                table: "Instrutores",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "PilarId",
                table: "Instrutores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instrutores_PilarId",
                table: "Instrutores",
                column: "PilarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrutores_Pilares_PilarId",
                table: "Instrutores",
                column: "PilarId",
                principalTable: "Pilares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instrutores_Pilares_PilarId",
                table: "Instrutores");

            migrationBuilder.DropIndex(
                name: "IX_Instrutores_PilarId",
                table: "Instrutores");

            migrationBuilder.DropColumn(
                name: "Pilar",
                table: "Instrutores");

            migrationBuilder.DropColumn(
                name: "PilarId",
                table: "Instrutores");

            migrationBuilder.AddColumn<int>(
                name: "Id_Pilar",
                table: "Instrutores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Instrutores_Id_Pilar",
                table: "Instrutores",
                column: "Id_Pilar");

            migrationBuilder.AddForeignKey(
                name: "FK_Instrutores_Pilares_Id_Pilar",
                table: "Instrutores",
                column: "Id_Pilar",
                principalTable: "Pilares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
