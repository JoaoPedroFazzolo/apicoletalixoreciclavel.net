using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRepository;

        public NotificacaoService(INotificacaoRepository notificacaoRepository)
        {
            _notificacaoRepository = notificacaoRepository;
        }

        public IEnumerable<NotificacaoModel> ObterTodasNotificacoes(int pageNumber = 1, int pageSize = 10)
        {
            return _notificacaoRepository.GetAll(pageNumber, pageSize);
        }
        
        public IEnumerable<NotificacaoModel> ObterTodasNotificacoesComDetalhes(int pageNumber = 1, int pageSize = 10)
        {
            return _notificacaoRepository.GetAllWithDetails(pageNumber, pageSize);
        }

        public NotificacaoModel ObterNotificacaoPorId(long id)
        {
            return _notificacaoRepository.GetByIdWithDetails(id);
        }

        public IEnumerable<NotificacaoModel> ObterNotificacoesPorUsuario(long usuarioId)
        {
            return _notificacaoRepository.GetByUsuario(usuarioId);
        }

        public IEnumerable<NotificacaoModel> ObterNotificacoesPorStatus(string status)
        {
            return _notificacaoRepository.GetByStatus(status);
        }

        public IEnumerable<NotificacaoModel> ObterNotificacoesPorTipo(string tipo)
        {
            return _notificacaoRepository.GetByTipo(tipo);
        }

        public IEnumerable<NotificacaoModel> ObterNotificacaoNaoLidasPorUsuario(long usuarioId)
        {
            return _notificacaoRepository.GetNaoLidasByUsuario(usuarioId);
        }

        public void AdicionarNotificacao(NotificacaoModel notificacao)
        {
            if (string.IsNullOrEmpty(notificacao.Titulo))
            {
                throw new ArgumentException("O título da notificação é obrigatório.");
            }

            if (string.IsNullOrEmpty(notificacao.Mensagem))
            {
                throw new ArgumentException("A mensagem da notificação é obrigatória.");
            }

            notificacao.DataCriacao = DateTime.Now;
            notificacao.Status = "NaoLida";
            _notificacaoRepository.Add(notificacao);
        }

        public void AtualizarNotificacao(NotificacaoModel notificacao)
        {
            if (string.IsNullOrEmpty(notificacao.Titulo))
            {
                throw new ArgumentException("O título da notificação é obrigatório.");
            }

            if (string.IsNullOrEmpty(notificacao.Mensagem))
            {
                throw new ArgumentException("A mensagem da notificação é obrigatória.");
            }

            _notificacaoRepository.Update(notificacao);
        }

        public void DeletarNotificacao(long id)
        {
            _notificacaoRepository.Delete(id);
        }

        public void MarcarComoLida(long id)
        {
            _notificacaoRepository.MarcarComoLida(id);
        }

        public void MarcarComoArquivada(long id)
        {
            _notificacaoRepository.MarcarComoArquivada(id);
        }

        public int ContarNaoLidasPorUsuario(long usuarioId)
        {
            return _notificacaoRepository.CountNaoLidasByUsuario(usuarioId);
        }
    }
}