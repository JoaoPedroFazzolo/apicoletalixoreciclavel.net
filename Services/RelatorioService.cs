using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public IEnumerable<RelatorioModel> ObterTodosRelatorios(int pageNumber = 1, int pageSize = 10)
        {
            return _relatorioRepository.GetAll(pageNumber, pageSize);
        }

        public RelatorioModel ObterRelatorioPorId(long id)
        {
            return _relatorioRepository.GetById(id);
        }

        public void CriarRelatorio(RelatorioModel relatorio)
        {
            if (string.IsNullOrEmpty(relatorio.Nome))
            {
                throw new ArgumentException("O nome do relatório é obrigatório.");
            }
            
            relatorio.DataGeracao = DateTime.Now;
            _relatorioRepository.Add(relatorio);
        }

        public void AtualizarRelatorio(RelatorioModel relatorio)
        {
            if (string.IsNullOrEmpty(relatorio.Nome))
            {
                throw new ArgumentException("O nome do relatório é obrigatório.");
            }
            _relatorioRepository.Update(relatorio);
        }

        public void DeletarRelatorio(RelatorioModel relatorio)
        {
            _relatorioRepository.Delete(relatorio);
        }
    }
}