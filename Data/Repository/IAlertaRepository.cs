using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public interface IAlertaRepository
    {
        IEnumerable<AlertaModel> GetAll(int pageNumber = 1, int pageSize = 10);
        AlertaModel GetById(long id);
        IEnumerable<AlertaModel> GetByStatus(string status);
        IEnumerable<AlertaModel> GetByTipo(string tipo);
        IEnumerable<AlertaModel> GetByUsuario(long usuarioId);
        IEnumerable<AlertaModel> GetByPeriodo(DateTime dataInicio, DateTime dataFim);
        void Add(AlertaModel alerta);
        void Update(AlertaModel alerta);
        void Delete(AlertaModel alerta);
        void Delete(long id);
        long CountByStatus(string status);
    }
}