using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicoletalixoreciclavel.Migrations
{
    /// <inheritdoc />
    public partial class ExcluiDEstinacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResiduosEletronicos_Ponto_coleta_PontoColetaId",
                table: "ResiduosEletronicos");

            migrationBuilder.DropTable(
                name: "Destinacao");

            migrationBuilder.DropIndex(
                name: "IX_ResiduosEletronicos_PontoColetaId",
                table: "ResiduosEletronicos");

            migrationBuilder.DropColumn(
                name: "PontoColetaId",
                table: "ResiduosEletronicos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PontoColetaId",
                table: "ResiduosEletronicos",
                type: "NUMBER(19)",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Destinacao",
                columns: table => new
                {
                    DestinacaoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CapacidadeMaxima = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    DiasAtendimento = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    HorarioFuncionamentoFim = table.Column<string>(type: "VARCHAR2(8)", maxLength: 8, nullable: true),
                    HorarioFuncionamentoInicio = table.Column<string>(type: "VARCHAR2(8)", maxLength: 8, nullable: true),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Observacoes = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    PermiteColeta = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ResponsavelTecnico = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    UnidadeCapacidade = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinacao", x => x.DestinacaoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResiduosEletronicos_PontoColetaId",
                table: "ResiduosEletronicos",
                column: "PontoColetaId");

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
                name: "FK_ResiduosEletronicos_Ponto_coleta_PontoColetaId",
                table: "ResiduosEletronicos",
                column: "PontoColetaId",
                principalTable: "Ponto_coleta",
                principalColumn: "PontoColetaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
