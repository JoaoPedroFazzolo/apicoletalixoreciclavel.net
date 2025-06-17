using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Repository
{
    public class DestinacaoRepository : IDestinacaoRepository
    {
        private readonly DatabaseContext _context;

        public DestinacaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<DestinacaoModel> GetAll()
        {
            return _context.Destinacoes
                .OrderBy(d => d.Nome)
                .ToList();
        }

        public DestinacaoModel GetById(long id)
        {
            return _context.Destinacoes
                .FirstOrDefault(d => d.DestinacaoId == id);
        }

        public IEnumerable<DestinacaoModel> GetByTipo(string tipo)
        {
            return _context.Destinacoes
                .Where(d => d.Tipo.ToLower() == tipo.ToLower())
                .OrderBy(d => d.Nome)
                .ToList();
        }

        public IEnumerable<DestinacaoModel> GetByStatus(string status)
        {
            return _context.Destinacoes
                .Where(d => d.Status.ToLower() == status.ToLower())
                .OrderBy(d => d.Nome)
                .ToList();
        }

        public IEnumerable<DestinacaoModel> GetAtivas()
        {
            return _context.Destinacoes
                .Where(d => d.Status == "Ativo")
                .OrderBy(d => d.Nome)
                .ToList();
        }

        public IEnumerable<DestinacaoModel> GetQuePermitemColeta()
        {
            return _context.Destinacoes
                .Where(d => d.PermiteColeta && d.Status == "Ativo")
                .OrderBy(d => d.Nome)
                .ToList();
        }

        public IEnumerable<DestinacaoModel> SearchByName(string nome)
        {
            return _context.Destinacoes
                .Where(d => d.Nome.ToLower().Contains(nome.ToLower()))
                .OrderBy(d => d.Nome)
                .ToList();
        }

        public void Add(DestinacaoModel destinacao)
        {
            _context.Destinacoes.Add(destinacao);
            _context.SaveChanges();
        }

        public void Update(DestinacaoModel destinacao)
        {
            destinacao.DataAtualizacao = DateTime.Now;
            _context.Destinacoes.Update(destinacao);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var destinacao = GetById(id);
            if (destinacao != null)
            {
                _context.Destinacoes.Remove(destinacao);
                _context.SaveChanges();
            }
        }

        public void AlterarStatus(long id, string status)
        {
            var destinacao = GetById(id);
            if (destinacao != null)
            {
                destinacao.Status = status;
                destinacao.DataAtualizacao = DateTime.Now;
                Update(destinacao);
            }
        }

        public bool ExistsById(long id)
        {
            return _context.Destinacoes.Any(d => d.DestinacaoId == id);
        }

        public bool ExistsByName(string nome)
        {
            return _context.Destinacoes.Any(d => d.Nome.ToLower() == nome.ToLower());
        }

        public IEnumerable<string> GetAllTipos()
        {
            return _context.Destinacoes
                .Select(d => d.Tipo)
                .Distinct()
                .OrderBy(t => t)
                .ToList();
        }
    }
}