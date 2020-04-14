using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    local = table.Column<string>(nullable: true),
                    dataEvento = table.Column<DateTime>(nullable: false),
                    tema = table.Column<string>(nullable: true),
                    qtdPessoas = table.Column<int>(nullable: false),
                    imagemURL = table.Column<string>(nullable: true),
                    telefone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Oradores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    miniCurriculo = table.Column<string>(nullable: true),
                    imagemURL = table.Column<string>(nullable: true),
                    telefone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oradores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    preco = table.Column<decimal>(nullable: false),
                    dataInicio = table.Column<DateTime>(nullable: true),
                    dataFim = table.Column<DateTime>(nullable: true),
                    quantidade = table.Column<int>(nullable: false),
                    eventoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lotes_Eventos_eventoId",
                        column: x => x.eventoId,
                        principalTable: "Eventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OradorEventos",
                columns: table => new
                {
                    oradorId = table.Column<int>(nullable: false),
                    eventoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OradorEventos", x => new { x.eventoId, x.oradorId });
                    table.ForeignKey(
                        name: "FK_OradorEventos_Eventos_eventoId",
                        column: x => x.eventoId,
                        principalTable: "Eventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OradorEventos_Oradores_oradorId",
                        column: x => x.oradorId,
                        principalTable: "Oradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedesSociais",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    eventoId = table.Column<int>(nullable: true),
                    oradorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedesSociais", x => x.id);
                    table.ForeignKey(
                        name: "FK_RedesSociais_Eventos_eventoId",
                        column: x => x.eventoId,
                        principalTable: "Eventos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RedesSociais_Oradores_oradorId",
                        column: x => x.oradorId,
                        principalTable: "Oradores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_eventoId",
                table: "Lotes",
                column: "eventoId");

            migrationBuilder.CreateIndex(
                name: "IX_OradorEventos_oradorId",
                table: "OradorEventos",
                column: "oradorId");

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociais_eventoId",
                table: "RedesSociais",
                column: "eventoId");

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociais_oradorId",
                table: "RedesSociais",
                column: "oradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "OradorEventos");

            migrationBuilder.DropTable(
                name: "RedesSociais");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Oradores");
        }
    }
}
