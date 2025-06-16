using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public interface IDestinacaoService
    {
        IEnumerable<DestinacaoModel> ObterTodasDestinacoes();
        DestinacaoModel ObterDestinacaoPorId(long id);
        IEnumerable<DestinacaoModel> ObterDestinacoesPorTipo(string tipo);
        IEnumerable<DestinacaoModel> ObterDestinacoesPorStatus(string status);
        IEnumerable<DestinacaoModel> ObterDestinacoesAtivas();
        IEnumerable<DestinacaoModel> ObterDestinacoesQuePermitemColeta();
        IEnumerable<DestinacaoModel> PesquisarPorNome(string nome);
        void AdicionarDestinacao(DestinacaoModel destinacao);
        void AtualizarDestinacao(DestinacaoModel destinacao);
        void DeletarDestinacao(long id);
        void AlterarStatus(long id, string status);
        IEnumerable<string> ObterTodosTipos();
    }
}