using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public interface IColetaService
{
    IEnumerable<ColetaModel> ObterTodasColetas();
    IEnumerable<ColetaModel> ObterTodasColetasComDetalhes();
    ColetaModel ObterColetaPorId(long coletaId);
    ColetaModel ObterColetaPorIdComDetalhes(long coletaId);
    void AdicionarColeta(ColetaModel coletaModel);
    void AtualizarColeta(ColetaModel coletaModel);
    void DeletarColeta(long coletaId);
}