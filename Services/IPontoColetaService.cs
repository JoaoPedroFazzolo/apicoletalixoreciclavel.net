using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public interface IPontoColetaService
{
    IEnumerable<PontoColetaModel> ObterTodosPontosColetas(int pageNumber = 1, int pageSize = 10);
    IEnumerable<PontoColetaModel> ObterTodosPontosColetasComDetalhes(int pageNumber = 1, int pageSize = 10);
    PontoColetaModel ObterPontoColetaPorId(long pontoColetaId);
    PontoColetaModel ObterPontoColetaPorIdComDetalhes(long pontoColetaId);
    void AdicionarPontoColeta(PontoColetaModel pontoColetaModel);
    void AtualizarPontoColeta(PontoColetaModel pontoColetaModel);
    void DeletarPontoColeta(long pontoColetaId);
}