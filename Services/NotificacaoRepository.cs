using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Repository
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly DatabaseContext _context;

        public NotificacaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<NotificacaoModel> GetAll()
        {
            return _context.Notificacoes
                .OrderByDescending(n => n.DataCriacao)
                .ToList();
        }

        public IEnumerable<NotificacaoModel> GetAllWithDetails()
        {
            return _context.Notificacoes
                .Include(n => n.Usuario)
                .OrderByDescending(n => n.DataCriacao)
                .ToList();
        }

        public NotificacaoModel GetById(long id)
        {
            return _context.Notificacoes
                .FirstOrDefault(n => n.NotificacaoId == id);
        }

        public NotificacaoModel GetByIdWithDetails(long id)
        {
            return _context.Notificacoes
                .Include(n => n.Usuario)
                .FirstOrDefault(n => n.NotificacaoId == id);
        }

        public IEnumerable<NotificacaoModel> GetByUsuario(long usuarioId)
        {
            return _context.Notificacoes
                .Include(n => n.Usuario)
                .Where(n => n.UsuarioId == usuarioId)
                .OrderByDescending(n => n.DataCriacao)
                .ToList();
        }

        public IEnumerable<NotificacaoModel> GetByStatus(string status)
        {
            return _context.Notificacoes
                .Include(n => n.Usuario)
                .Where(n => n.Status == status)
                .OrderByDescending(n => n.DataCriacao)
                .ToList();
        }

        public IEnumerable<NotificacaoModel> GetByTipo(string tipo)
        {
            return _context.Notificacoes
                .Include(n => n.Usuario)
                .Where(n => n.Tipo == tipo)
                .OrderByDescending(n => n.DataCriacao)
                .ToList();
        }

        public IEnumerable<NotificacaoModel> GetNaoLidasByUsuario(long usuarioId)
        {
            return _context.Notificacoes
                .Include(n => n.Usuario)
                .Where(n => n.UsuarioId == usuarioId && n.Status == "NaoLida")
                .OrderByDescending(n => n.DataCriacao)
                .ToList();
        }

        public void Add(NotificacaoModel notificacao)
        {
            _context.Notificacoes.Add(notificacao);
            _context.SaveChanges();
        }

        public void Update(NotificacaoModel notificacao)
        {
            _context.Notificacoes.Update(notificacao);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var notificacao = GetById(id);
            if (notificacao != null)
            {
                _context.Notificacoes.Remove(notificacao);
                _context.SaveChanges();
            }
        }

        public void MarcarComoLida(long id)
        {
            var notificacao = GetById(id);
            if (notificacao != null && notificacao.Status == "NaoLida")
            {
                notificacao.Status = "Lida";
                notificacao.DataLeitura = DateTime.Now;
                Update(notificacao);
            }
        }

        public void MarcarComoArquivada(long id)
        {
            var notificacao = GetById(id);
            if (notificacao != null)
            {
                notificacao.Status = "Arquivada";
                Update(notificacao);
            }
        }

        public int CountNaoLidasByUsuario(long usuarioId)
        {
            return _context.Notificacoes
                .Count(n => n.UsuarioId == usuarioId && n.Status == "NaoLida");
        }
    }
}