using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository;

public interface IPontoColetaRepository
{
    IEnumerable<PontoColetaModel> GetAll(int pageNumber = 1, int pageSize = 10);
    IEnumerable<PontoColetaModel> GetAllWithDetails(int pageNumber = 1, int pageSize = 10);
    PontoColetaModel GetById(long id);
    PontoColetaModel GetByIdWithDetails(long id);
    void Add(PontoColetaModel pontoColetaModel);
    void Update(PontoColetaModel pontoColetaModel);
    void Delete(long id);
}