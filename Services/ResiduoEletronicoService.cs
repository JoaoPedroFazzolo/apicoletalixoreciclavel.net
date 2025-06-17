using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public class ResiduoEletronicoService : IResiduoEletronicoService
{
    private readonly IResiduoEletronicoRepository _residuoEletronicoRepository;

    public ResiduoEletronicoService(IResiduoEletronicoRepository residuoEletronicoRepository)
    {
        _residuoEletronicoRepository = residuoEletronicoRepository;
    }

    public IEnumerable<ResiduoEletronicoModel> ObterTodosResiduoEletronicos(int pageNumber = 1, int pageSize = 10)
    {
        return _residuoEletronicoRepository.GetAll(pageNumber, pageSize);
    }

    public IEnumerable<ResiduoEletronicoModel> ObterTodosResiduoEletronicosComDetalhes(int pageNumber = 1, int pageSize = 10)
    {
        return _residuoEletronicoRepository.GetAllWithDetails(pageNumber, pageSize);
    }

    public ResiduoEletronicoModel ObterResiduoEletronicoPorId(long residuoEletronicoId)
    {
        return _residuoEletronicoRepository.GetById(residuoEletronicoId);
    }

    public ResiduoEletronicoModel ObterResiduoEletronicoPorIdComDetalhes(long residuoEletronicoId)
    {
        return _residuoEletronicoRepository.GetByIdWithDetails(residuoEletronicoId);
    }

    public void AdicionarResiduoEletronico(ResiduoEletronicoModel residuoEletronicoModel)
    {
        _residuoEletronicoRepository.Add(residuoEletronicoModel);
    }

    public void AtualizarResiduoEletronico(ResiduoEletronicoModel residuoEletronicoModel)
    {
        if (residuoEletronicoModel == null)
        {
            throw new ArgumentException("ResiduoEletronicoModel is null");
        }
        _residuoEletronicoRepository.Update(residuoEletronicoModel);
    }

    public void DeletarResiduoEletronico(long residuoEletronicoModel)
    {
        _residuoEletronicoRepository.Delete(residuoEletronicoModel);
    }
}