using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public interface IRelatorioRepository
    {
    IEnumerable<RelatorioModel> GetAll(int pageNumber = 1, int pageSize = 10);
        RelatorioModel GetById(long id);
        void Add(RelatorioModel relatorio);
        void Update(RelatorioModel relatorio);
        void Delete(RelatorioModel relatorio);
    }
}