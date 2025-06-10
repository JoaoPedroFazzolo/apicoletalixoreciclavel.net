using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicoletalixoreciclavel.Migrations
{
    /// <inheritdoc />
    public partial class FixRelacionamentoUsuarioResíduoNovamente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResiduosEletronicos_usuario_UsuarioModelUsuarioId",
                table: "ResiduosEletronicos");

            migrationBuilder.DropIndex(
                name: "IX_ResiduosEletronicos_UsuarioModelUsuarioId",
                table: "ResiduosEletronicos");

            migrationBuilder.DropColumn(
                name: "UsuarioModelUsuarioId",
                table: "ResiduosEletronicos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UsuarioModelUsuarioId",
                table: "ResiduosEletronicos",
                type: "NUMBER(19)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResiduosEletronicos_UsuarioModelUsuarioId",
                table: "ResiduosEletronicos",
                column: "UsuarioModelUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResiduosEletronicos_usuario_UsuarioModelUsuarioId",
                table: "ResiduosEletronicos",
                column: "UsuarioModelUsuarioId",
                principalTable: "usuario",
                principalColumn: "UsuarioId");
        }
    }
}
