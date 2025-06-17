using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public interface IDestinacaoRepository
    {
        IEnumerable<DestinacaoModel> GetAll();
        DestinacaoModel GetById(long id);
        IEnumerable<DestinacaoModel> GetByTipo(string tipo);
        IEnumerable<DestinacaoModel> GetByStatus(string status);
        IEnumerable<DestinacaoModel> GetAtivas();
        IEnumerable<DestinacaoModel> GetQuePermitemColeta();
        IEnumerable<DestinacaoModel> SearchByName(string nome);
        void Add(DestinacaoModel destinacao);
        void Update(DestinacaoModel destinacao);
        void Delete(long id);
        void AlterarStatus(long id, string status);
        bool ExistsById(long id);
        bool ExistsByName(string nome);
        IEnumerable<string> GetAllTipos();
    }
}