using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository;

public interface IPontoColetaRepository
{
    IEnumerable<PontoColetaModel> GetAll();
    IEnumerable<PontoColetaModel> GetAllWithDetails();
    PontoColetaModel GetById(long id);
    PontoColetaModel GetByIdWithDetails(long id);
    void Add(PontoColetaModel pontoColetaModel);
    void Update(PontoColetaModel pontoColetaModel);
    void Delete(long id);
}