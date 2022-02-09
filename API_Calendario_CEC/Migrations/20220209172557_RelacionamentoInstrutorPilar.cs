using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace API_Calendario_CEC.Migrations
{
    public partial class RelacionamentoInstrutorPilar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PilaresInstrutores");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PilaresInstrutores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Id_Instrutor = table.Column<int>(type: "int", nullable: false),
                    Id_Pilar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PilaresInstrutores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PilaresInstrutores_Instrutores_Id_Instrutor",
                        column: x => x.Id_Instrutor,
                        principalTable: "Instrutores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PilaresInstrutores_Pilares_Id_Pilar",
                        column: x => x.Id_Pilar,
                        principalTable: "Pilares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PilaresInstrutores_Id_Instrutor",
                table: "PilaresInstrutores",
                column: "Id_Instrutor");

            migrationBuilder.CreateIndex(
                name: "IX_PilaresInstrutores_Id_Pilar",
                table: "PilaresInstrutores",
                column: "Id_Pilar");
        }
    }
}
