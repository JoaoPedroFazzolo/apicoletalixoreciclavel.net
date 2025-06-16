using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicoletalixoreciclavel.Migrations
{
    /// <inheritdoc />
    public partial class CreateColetaAndPontoColetaTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ponto_coleta",
                columns: table => new
                {
                    PontoColetaId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Capacidade = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ponto_coleta", x => x.PontoColetaId);
                });

            migrationBuilder.CreateTable(
                name: "coleta",
                columns: table => new
                {
                    ColetaId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataColeta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ResiduoId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    PontoColetaId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coleta", x => x.ColetaId);
                    table.ForeignKey(
                        name: "FK_coleta_ResiduosEletronicos_ResiduoId",
                        column: x => x.ResiduoId,
                        principalTable: "ResiduosEletronicos",
                        principalColumn: "ResiduoEletronicoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_coleta_ponto_coleta_PontoColetaId",
                        column: x => x.PontoColetaId,
                        principalTable: "ponto_coleta",
                        principalColumn: "PontoColetaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_coleta_PontoColetaId",
                table: "coleta",
                column: "PontoColetaId");

            migrationBuilder.CreateIndex(
                name: "IX_coleta_ResiduoId",
                table: "coleta",
                column: "ResiduoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coleta");

            migrationBuilder.DropTable(
                name: "ponto_coleta");
        }
    }
}
