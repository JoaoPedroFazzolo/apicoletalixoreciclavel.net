using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository;

public interface IResiduoEletronicoRepository
{
    IEnumerable<ResiduoEletronicoModel> GetAll(int pageNumber = 1, int pageSize = 10);
    IEnumerable<ResiduoEletronicoModel> GetAllWithDetails(int pageNumber = 1, int pageSize = 10);
    ResiduoEletronicoModel GetById(long id);
    ResiduoEletronicoModel GetByIdWithDetails(long usuarioId);
    void Add(ResiduoEletronicoModel residuoEletronicoModel);
    void Update(ResiduoEletronicoModel residuoEletronicoModel);
    void Delete(long residuoEletronicoModel);
}