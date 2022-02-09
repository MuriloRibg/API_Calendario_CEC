using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace API_Calendario_CEC.Migrations
{
    public partial class Tabelas_Relacionamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Id_Pilar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Pilares_Id_Pilar",
                        column: x => x.Id_Pilar,
                        principalTable: "Pilares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PilaresInstrutores_Pilares_Id_Pilar",
                        column: x => x.Id_Pilar,
                        principalTable: "Pilares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime", maxLength: 20, nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime", maxLength: 20, nullable: false),
                    Id_Local = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Locais_Id_Local",
                        column: x => x.Id_Local,
                        principalTable: "Locais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Id_Instrutor = table.Column<int>(type: "int", nullable: false),
                    Id_Turma = table.Column<int>(type: "int", nullable: false),
                    Id_Disciplina = table.Column<int>(type: "int", nullable: false),
                    Id_Reserva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aulas_Disciplinas_Id_Disciplina",
                        column: x => x.Id_Disciplina,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aulas_Instrutores_Id_Instrutor",
                        column: x => x.Id_Instrutor,
                        principalTable: "Instrutores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aulas_Reservas_Id_Reserva",
                        column: x => x.Id_Reserva,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aulas_Turmas_Id_Turma",
                        column: x => x.Id_Turma,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Id_Instrutor = table.Column<int>(type: "int", nullable: false),
                    Id_Reserva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Instrutores_Id_Instrutor",
                        column: x => x.Id_Instrutor,
                        principalTable: "Instrutores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eventos_Reservas_Id_Reserva",
                        column: x => x.Id_Reserva,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Id_Disciplina",
                table: "Aulas",
                column: "Id_Disciplina");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Id_Instrutor",
                table: "Aulas",
                column: "Id_Instrutor");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Id_Reserva",
                table: "Aulas",
                column: "Id_Reserva");

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_Id_Turma",
                table: "Aulas",
                column: "Id_Turma");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_Id_Pilar",
                table: "Disciplinas",
                column: "Id_Pilar");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Id_Instrutor",
                table: "Eventos",
                column: "Id_Instrutor");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_Id_Reserva",
                table: "Eventos",
                column: "Id_Reserva");

            migrationBuilder.CreateIndex(
                name: "IX_PilaresInstrutores_Id_Instrutor",
                table: "PilaresInstrutores",
                column: "Id_Instrutor");

            migrationBuilder.CreateIndex(
                name: "IX_PilaresInstrutores_Id_Pilar",
                table: "PilaresInstrutores",
                column: "Id_Pilar");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_Id_Local",
                table: "Reservas",
                column: "Id_Local");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "PilaresInstrutores");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Reservas");
        }
    }
}
