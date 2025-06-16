using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public interface INotificacaoService
    {
        IEnumerable<NotificacaoModel> ObterTodasNotificacoes();
        NotificacaoModel ObterNotificacaoPorId(long id);
        IEnumerable<NotificacaoModel> ObterNotificacoesPorUsuario(long usuarioId);
        IEnumerable<NotificacaoModel> ObterNotificacoesPorStatus(string status);
        IEnumerable<NotificacaoModel> ObterNotificacoesPorTipo(string tipo);
        IEnumerable<NotificacaoModel> ObterNotificacaoNaoLidasPorUsuario(long usuarioId);
        void AdicionarNotificacao(NotificacaoModel notificacao);
        void AtualizarNotificacao(NotificacaoModel notificacao);
        void DeletarNotificacao(long id);
        void MarcarComoLida(long id);
        void MarcarComoArquivada(long id);
        int ContarNaoLidasPorUsuario(long usuarioId);
    }
}