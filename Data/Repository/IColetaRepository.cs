using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository;

public interface IColetaRepository
{
    IEnumerable<ColetaModel> GetAll();
    IEnumerable<ColetaModel> GetAllWithDetails();
    ColetaModel GetById(long id);
    ColetaModel GetByIdWithDetails(long id);
    void Add(ColetaModel coletaModel);
    void Update(ColetaModel coletaModel);
    void Delete(long id);
}