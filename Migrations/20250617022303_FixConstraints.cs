using Microsoft.EntityFrameworkCore.Migrations;

public partial class FixConstraints : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Verifique se a constraint existe antes de tentar removê-la
        migrationBuilder.Sql(@"
            BEGIN
                EXECUTE IMMEDIATE 'ALTER TABLE Coleta DROP CONSTRAINT FK_Coleta_Ponto_Coleta_PontoColetaId';
            EXCEPTION
                WHEN OTHERS THEN
                    IF SQLCODE != -2443 THEN -- ORA-02443: Cannot drop constraint - nonexistent constraint
                        RAISE;
                    END IF;
            END;
        ");

        // Adicione novamente a constraint, se necessário
        migrationBuilder.AddForeignKey(
            name: "FK_Coleta_Ponto_Coleta_PontoColetaId",
            table: "Coleta",
            column: "PontoColetaId",
            principalTable: "PontoColeta",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Remova a constraint novamente no método Down
        migrationBuilder.DropForeignKey(
            name: "FK_Coleta_Ponto_Coleta_PontoColetaId",
            table: "Coleta"
        );
    }
}