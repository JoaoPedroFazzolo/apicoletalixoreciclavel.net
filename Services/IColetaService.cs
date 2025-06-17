using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public interface IColetaService
{
    IEnumerable<ColetaModel> ObterTodasColetas(int pageNumber = 1, int pageSize = 10);
    IEnumerable<ColetaModel> ObterTodasColetasComDetalhes(int pageNumber = 1, int pageSize = 10);
    ColetaModel ObterColetaPorId(long coletaId);
    ColetaModel ObterColetaPorIdComDetalhes(long coletaId);
    void AdicionarColeta(ColetaModel coletaModel);
    void AtualizarColeta(ColetaModel coletaModel);
    void DeletarColeta(long coletaId);
}