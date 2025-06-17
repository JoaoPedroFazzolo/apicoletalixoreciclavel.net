using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public interface IEmpresaDestinacaoRepository
    {
        IEnumerable<EmpresaDestinacaoModel> GetAll();
        EmpresaDestinacaoModel GetById(long id);
        void Add(EmpresaDestinacaoModel empresaDestinacao);
        void Update(EmpresaDestinacaoModel empresaDestinacao);
        void Delete(long id);
        IEnumerable<EmpresaDestinacaoModel> SearchByName(string nome);
        bool ExistsByName(string nome);
    }
}