using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicoletalixoreciclavel.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coleta_ResiduosEletronicos_ResiduoId",
                table: "coleta");

            migrationBuilder.DropForeignKey(
                name: "FK_coleta_ponto_coleta_PontoColetaId",
                table: "coleta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ponto_coleta",
                table: "ponto_coleta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_coleta",
                table: "coleta");

            migrationBuilder.RenameTable(
                name: "ponto_coleta",
                newName: "Ponto_Coleta");

            migrationBuilder.RenameTable(
                name: "coleta",
                newName: "Coleta");

            migrationBuilder.RenameIndex(
                name: "IX_coleta_ResiduoId",
                table: "Coleta",
                newName: "IX_Coleta_ResiduoId");

            migrationBuilder.RenameIndex(
                name: "IX_coleta_PontoColetaId",
                table: "Coleta",
                newName: "IX_Coleta_PontoColetaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ponto_Coleta",
                table: "Ponto_Coleta",
                column: "PontoColetaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coleta",
                table: "Coleta",
                column: "ColetaId");

            migrationBuilder.CreateTable(
                name: "Destinacao",
                columns: table => new
                {
                    DestinacaoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    ResponsavelTecnico = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CapacidadeMaxima = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnidadeCapacidade = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Observacoes = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    PermiteColeta = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    HorarioFuncionamentoInicio = table.Column<string>(type: "VARCHAR2(8)", maxLength: 8, nullable: true),
                    HorarioFuncionamentoFim = table.Column<string>(type: "VARCHAR2(8)", maxLength: 8, nullable: true),
                    DiasAtendimento = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinacao", x => x.DestinacaoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destinacao_Nome",
                table: "Destinacao",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Destinacao_Status",
                table: "Destinacao",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Destinacao_Tipo",
                table: "Destinacao",
                column: "Tipo");

            migrationBuilder.AddForeignKey(
                name: "FK_Coleta_Ponto_Coleta_PontoColetaId",
                table: "Coleta",
                column: "PontoColetaId",
                principalTable: "Ponto_Coleta",
                principalColumn: "PontoColetaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coleta_ResiduosEletronicos_ResiduoId",
                table: "Coleta",
                column: "ResiduoId",
                principalTable: "ResiduosEletronicos",
                principalColumn: "ResiduoEletronicoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coleta_Ponto_Coleta_PontoColetaId",
                table: "Coleta");

            migrationBuilder.DropForeignKey(
                name: "FK_Coleta_ResiduosEletronicos_ResiduoId",
                table: "Coleta");

            migrationBuilder.DropTable(
                name: "Destinacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ponto_Coleta",
                table: "Ponto_Coleta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coleta",
                table: "Coleta");

            migrationBuilder.RenameTable(
                name: "Ponto_Coleta",
                newName: "ponto_coleta");

            migrationBuilder.RenameTable(
                name: "Coleta",
                newName: "coleta");

            migrationBuilder.RenameIndex(
                name: "IX_Coleta_ResiduoId",
                table: "coleta",
                newName: "IX_coleta_ResiduoId");

            migrationBuilder.RenameIndex(
                name: "IX_Coleta_PontoColetaId",
                table: "coleta",
                newName: "IX_coleta_PontoColetaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ponto_coleta",
                table: "ponto_coleta",
                column: "PontoColetaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_coleta",
                table: "coleta",
                column: "ColetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_coleta_ResiduosEletronicos_ResiduoId",
                table: "coleta",
                column: "ResiduoId",
                principalTable: "ResiduosEletronicos",
                principalColumn: "ResiduoEletronicoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_coleta_ponto_coleta_PontoColetaId",
                table: "coleta",
                column: "PontoColetaId",
                principalTable: "ponto_coleta",
                principalColumn: "PontoColetaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
