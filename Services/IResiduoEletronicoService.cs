using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public interface IResiduoEletronicoService
{
    IEnumerable<ResiduoEletronicoModel> ObterTodosResiduoEletronicos();
    IEnumerable<ResiduoEletronicoModel> ObterTodosResiduoEletronicosComDetalhes();
    ResiduoEletronicoModel ObterResiduoEletronicoPorId(long residuoEletronicoId);
    ResiduoEletronicoModel ObterResiduoEletronicoPorIdComDetalhes(long residuoEletronicoId);
    void AdicionarResiduoEletronico(ResiduoEletronicoModel residuoEletronicoModel);
    void AtualizarResiduoEletronico(ResiduoEletronicoModel residuoEletronicoModel);
    void DeletarResiduoEletronico(long residuoEletronicoModel);


}