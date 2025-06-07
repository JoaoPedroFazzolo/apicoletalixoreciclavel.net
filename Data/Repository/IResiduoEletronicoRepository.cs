using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository;

public interface IResiduoEletronicoRepository
{
    IEnumerable<ResiduoEletronicoModel> GetAll();
    IEnumerable<ResiduoEletronicoModel> GetAllWithDetails();
    ResiduoEletronicoModel GetById(long id);
    ResiduoEletronicoModel GetByIdWithDetails(long usuarioId);
    void Add(ResiduoEletronicoModel residuoEletronicoModel);
    void Update(ResiduoEletronicoModel residuoEletronicoModel);
    void Delete(long residuoEletronicoModel);
}