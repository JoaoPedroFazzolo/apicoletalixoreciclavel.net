using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public interface IRelatorioRepository
    {
    IEnumerable<RelatorioModel> GetAll();
        RelatorioModel GetById(long id);
        void Add(RelatorioModel relatorio);
        void Update(RelatorioModel relatorio);
        void Delete(RelatorioModel relatorio);
    }
}