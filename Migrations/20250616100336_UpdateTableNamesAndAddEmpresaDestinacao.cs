using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicoletalixoreciclavel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableNamesAndAddEmpresaDestinacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coleta_Ponto_Coleta_PontoColetaId",
                table: "Coleta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ponto_Coleta",
                table: "Ponto_Coleta");

            migrationBuilder.RenameTable(
                name: "Ponto_Coleta",
                newName: "Ponto_coleta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ponto_coleta",
                table: "Ponto_coleta",
                column: "PontoColetaId");

            migrationBuilder.CreateTable(
                name: "Empresa_destinacao",
                columns: table => new
                {
                    EmpresaDestinacaoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSDATE"),
                    DataAtualizacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa_destinacao", x => x.EmpresaDestinacaoId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Coleta_Ponto_coleta_PontoColetaId",
                table: "Coleta",
                column: "PontoColetaId",
                principalTable: "Ponto_coleta",
                principalColumn: "PontoColetaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coleta_Ponto_coleta_PontoColetaId",
                table: "Coleta");

            migrationBuilder.DropTable(
                name: "Empresa_destinacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ponto_coleta",
                table: "Ponto_coleta");

            migrationBuilder.RenameTable(
                name: "Ponto_coleta",
                newName: "Ponto_Coleta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ponto_Coleta",
                table: "Ponto_Coleta",
                column: "PontoColetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coleta_Ponto_Coleta_PontoColetaId",
                table: "Coleta",
                column: "PontoColetaId",
                principalTable: "Ponto_Coleta",
                principalColumn: "PontoColetaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
