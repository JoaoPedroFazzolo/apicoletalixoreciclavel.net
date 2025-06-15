using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public interface IPontoColetaService
{
    IEnumerable<PontoColetaModel> ObterTodosPontosColetas();
    IEnumerable<PontoColetaModel> ObterTodosPontosColetasComDetalhes();
    PontoColetaModel ObterPontoColetaPorId(long pontoColetaId);
    PontoColetaModel ObterPontoColetaPorIdComDetalhes(long pontoColetaId);
    void AdicionarPontoColeta(PontoColetaModel pontoColetaModel);
    void AtualizarPontoColeta(PontoColetaModel pontoColetaModel);
    void DeletarPontoColeta(long pontoColetaId);
}