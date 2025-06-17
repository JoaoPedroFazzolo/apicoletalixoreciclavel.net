using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public interface INotificacaoRepository
    {
        IEnumerable<NotificacaoModel> GetAll(int pageNumber = 1, int pageSize = 10);
        IEnumerable<NotificacaoModel> GetAllWithDetails(int pageNumber = 1, int pageSize = 10);
        NotificacaoModel GetById(long id);
        NotificacaoModel GetByIdWithDetails(long id);
        IEnumerable<NotificacaoModel> GetByUsuario(long usuarioId);
        IEnumerable<NotificacaoModel> GetByStatus(string status);
        IEnumerable<NotificacaoModel> GetByTipo(string tipo);
        IEnumerable<NotificacaoModel> GetNaoLidasByUsuario(long usuarioId);
        void Add(NotificacaoModel notificacao);
        void Update(NotificacaoModel notificacao);
        void Delete(long id);
        void MarcarComoLida(long id);
        void MarcarComoArquivada(long id);
        int CountNaoLidasByUsuario(long usuarioId);
    }
}