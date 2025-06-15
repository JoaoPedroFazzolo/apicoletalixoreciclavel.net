using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Repository
{
    public class AlertaRepository : IAlertaRepository
    {
        private readonly DatabaseContext _context;

        public AlertaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<AlertaModel> GetAll()
        {
            return _context.Alertas
                .Include(a => a.Usuario)
                .OrderByDescending(a => a.DataAlerta)
                .ToList();
        }

        public AlertaModel GetById(long id)
        {
            return _context.Alertas
                .Include(a => a.Usuario)
                .FirstOrDefault(a => a.AlertaId == id);
        }

        public IEnumerable<AlertaModel> GetByStatus(string status)
        {
            return _context.Alertas
                .Include(a => a.Usuario)
                .Where(a => a.Status == status)
                .OrderByDescending(a => a.DataAlerta)
                .ToList();
        }

        public IEnumerable<AlertaModel> GetByTipo(string tipo)
        {
            return _context.Alertas
                .Include(a => a.Usuario)
                .Where(a => a.TipoAlerta == tipo)
                .OrderByDescending(a => a.DataAlerta)
                .ToList();
        }

        public IEnumerable<AlertaModel> GetByUsuario(long usuarioId)
        {
            return _context.Alertas
                .Include(a => a.Usuario)
                .Where(a => a.UsuarioId == usuarioId)
                .OrderByDescending(a => a.DataAlerta)
                .ToList();
        }

        public IEnumerable<AlertaModel> GetByPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            return _context.Alertas
                .Include(a => a.Usuario)
                .Where(a => a.DataAlerta >= dataInicio && a.DataAlerta <= dataFim)
                .OrderByDescending(a => a.DataAlerta)
                .ToList();
        }

        public void Add(AlertaModel alerta)
        {
            _context.Alertas.Add(alerta);
            _context.SaveChanges();
        }

        public void Update(AlertaModel alerta)
        {
            _context.Alertas.Update(alerta);
            _context.SaveChanges();
        }

        public void Delete(AlertaModel alerta)
        {
            _context.Alertas.Remove(alerta);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var alerta = GetById(id);
            if (alerta != null)
            {
                Delete(alerta);
            }
        }

        public long CountByStatus(string status)
        {
            return _context.Alertas.Count(a => a.Status == status);
        }
    }
}