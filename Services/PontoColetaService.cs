using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public class PontoColetaService : IPontoColetaService
{
    private readonly IPontoColetaRepository _pontoColetaRepository;

    public PontoColetaService(IPontoColetaRepository pontoColetaRepository)
    {
        _pontoColetaRepository = pontoColetaRepository;
    }

    public IEnumerable<PontoColetaModel> ObterTodosPontosColetas(int pageNumber = 1, int pageSize = 10)
    {
        return _pontoColetaRepository.GetAll(pageNumber, pageSize);
    }

    public IEnumerable<PontoColetaModel> ObterTodosPontosColetasComDetalhes(int pageNumber = 1, int pageSize = 10)
    {
        return _pontoColetaRepository.GetAllWithDetails(pageNumber, pageSize);
    }

    public PontoColetaModel ObterPontoColetaPorId(long pontoColetaId)
    {
        return _pontoColetaRepository.GetById(pontoColetaId);
    }

    public PontoColetaModel ObterPontoColetaPorIdComDetalhes(long pontoColetaId)
    {
        return _pontoColetaRepository.GetByIdWithDetails(pontoColetaId);
    }

    public void AdicionarPontoColeta(PontoColetaModel pontoColetaModel)
    {
        _pontoColetaRepository.Add(pontoColetaModel);
    }

    public void AtualizarPontoColeta(PontoColetaModel pontoColetaModel)
    {
        if (pontoColetaModel == null)
        {
            throw new ArgumentException("PontoColetaModel is null");
        }
        _pontoColetaRepository.Update(pontoColetaModel);
    }

    public void DeletarPontoColeta(long pontoColetaId)
    {
        _pontoColetaRepository.Delete(pontoColetaId);
    }
}