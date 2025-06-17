using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository;

public interface IColetaRepository
{
    IEnumerable<ColetaModel> GetAll(int pageNumber = 1, int pageSize = 10);
    IEnumerable<ColetaModel> GetAllWithDetails(int pageNumber = 1, int pageSize = 10);
    ColetaModel GetById(long id);
    ColetaModel GetByIdWithDetails(long id);
    void Add(ColetaModel coletaModel);
    void Update(ColetaModel coletaModel);
    void Delete(long id);
}