using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apicoletalixoreciclavel.Migrations
{
    /// <inheritdoc />
    public partial class usuario_ponto_coleta_id : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuario",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20,
                oldDefaultValue: "(00) 00000-0000");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255,
                oldDefaultValue: "senha123");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Usuario",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50,
                oldDefaultValue: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Usuario",
                type: "NVARCHAR2(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2)",
                oldMaxLength: 2,
                oldDefaultValue: "SP");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Usuario",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200,
                oldDefaultValue: "Endereço não informado");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Usuario",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50,
                oldDefaultValue: "Cidade não informada");

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Usuario",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldDefaultValue: "00000-000");

            migrationBuilder.AddColumn<long>(
                name: "PontoColetaId",
                table: "ResiduosEletronicos",
                type: "NUMBER(19)",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ponto_coleta",
                table: "Ponto_coleta",
                column: "PontoColetaId");

            migrationBuilder.CreateIndex(
                name: "IX_ResiduosEletronicos_PontoColetaId",
                table: "ResiduosEletronicos",
                column: "PontoColetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coleta_Ponto_coleta_PontoColetaId",
                table: "Coleta",
                column: "PontoColetaId",
                principalTable: "Ponto_coleta",
                principalColumn: "PontoColetaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResiduosEletronicos_Ponto_coleta_PontoColetaId",
                table: "ResiduosEletronicos",
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

            migrationBuilder.DropForeignKey(
                name: "FK_ResiduosEletronicos_Ponto_coleta_PontoColetaId",
                table: "ResiduosEletronicos");

            migrationBuilder.DropIndex(
                name: "IX_ResiduosEletronicos_PontoColetaId",
                table: "ResiduosEletronicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ponto_coleta",
                table: "Ponto_coleta");

            migrationBuilder.DropColumn(
                name: "PontoColetaId",
                table: "ResiduosEletronicos");

            migrationBuilder.RenameTable(
                name: "Ponto_coleta",
                newName: "Ponto_Coleta");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuario",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "(00) 00000-0000",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "senha123",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Usuario",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "User",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Usuario",
                type: "NVARCHAR2(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "SP",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Usuario",
                type: "NVARCHAR2(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "Endereço não informado",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Usuario",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Cidade não informada",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "Usuario",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "00000-000",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10);

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
