using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public interface IRelatorioService
    {
        IEnumerable<RelatorioModel> ObterTodosRelatorios();
        RelatorioModel ObterRelatorioPorId(long id);
        void CriarRelatorio(RelatorioModel relatorio);
        void AtualizarRelatorio(RelatorioModel relatorio);
        void DeletarRelatorio(RelatorioModel relatorio);
    }
}