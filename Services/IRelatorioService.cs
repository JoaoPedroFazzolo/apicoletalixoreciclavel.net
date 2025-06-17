using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public interface IRelatorioService
    {
        IEnumerable<RelatorioModel> ObterTodosRelatorios(int pageNumber = 1, int pageSize = 10);
        RelatorioModel ObterRelatorioPorId(long id);
        void CriarRelatorio(RelatorioModel relatorio);
        void AtualizarRelatorio(RelatorioModel relatorio);
        void DeletarRelatorio(RelatorioModel relatorio);
    }
}