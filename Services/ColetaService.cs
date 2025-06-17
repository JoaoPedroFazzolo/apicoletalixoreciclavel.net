using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public class ColetaService : IColetaService
{
    private readonly IColetaRepository _coletaRepository;

    public ColetaService(IColetaRepository coletaRepository)
    {
        _coletaRepository = coletaRepository;
    }

    public IEnumerable<ColetaModel> ObterTodasColetas(int pageNumber = 1, int pageSize = 10)
    {
        return _coletaRepository.GetAll(pageNumber, pageSize);
    }

    public IEnumerable<ColetaModel> ObterTodasColetasComDetalhes(int pageNumber = 1, int pageSize = 10)
    {
        return _coletaRepository.GetAllWithDetails(pageNumber, pageSize);
    }

    public ColetaModel ObterColetaPorId(long coletaId)
    {
        return _coletaRepository.GetById(coletaId);
    }

    public ColetaModel ObterColetaPorIdComDetalhes(long coletaId)
    {
        return _coletaRepository.GetByIdWithDetails(coletaId);
    }

    public void AdicionarColeta(ColetaModel coletaModel)
    {
        _coletaRepository.Add(coletaModel);
    }

    public void AtualizarColeta(ColetaModel coletaModel)
    {
        if (coletaModel == null)
        {
            throw new ArgumentException("ColetaModel is null");
        }
        _coletaRepository.Update(coletaModel);
    }

    public void DeletarColeta(long coletaId)
    {
        _coletaRepository.Delete(coletaId);
    }
}