using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public interface IResiduoEletronicoService
{
    IEnumerable<ResiduoEletronicoModel> ObterTodosResiduoEletronicos(int pageNumber = 1, int pageSize = 10);
    IEnumerable<ResiduoEletronicoModel> ObterTodosResiduoEletronicosComDetalhes(int pageNumber = 1, int pageSize = 10);
    ResiduoEletronicoModel ObterResiduoEletronicoPorId(long residuoEletronicoId);
    ResiduoEletronicoModel ObterResiduoEletronicoPorIdComDetalhes(long residuoEletronicoId);
    void AdicionarResiduoEletronico(ResiduoEletronicoModel residuoEletronicoModel);
    void AtualizarResiduoEletronico(ResiduoEletronicoModel residuoEletronicoModel);
    void DeletarResiduoEletronico(long residuoEletronicoModel);


}