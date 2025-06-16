using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public class EmpresaDestinacaoRepository : IEmpresaDestinacaoRepository
    {
        private readonly DatabaseContext _context;

        public EmpresaDestinacaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<EmpresaDestinacaoModel> GetAll()
        {
            return _context.EmpresasDestinacao.ToList();
        }

        public EmpresaDestinacaoModel GetById(long id)
        {
            return _context.EmpresasDestinacao.Find(id);
        }

        public void Add(EmpresaDestinacaoModel empresaDestinacao)
        {
            _context.EmpresasDestinacao.Add(empresaDestinacao);
            _context.SaveChanges();
        }

        public void Update(EmpresaDestinacaoModel empresaDestinacao)
        {
            empresaDestinacao.DataAtualizacao = DateTime.Now;
            _context.EmpresasDestinacao.Update(empresaDestinacao);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var empresaDestinacao = GetById(id);
            if (empresaDestinacao != null)
            {
                _context.EmpresasDestinacao.Remove(empresaDestinacao);
                _context.SaveChanges();
            }
        }

        public IEnumerable<EmpresaDestinacaoModel> SearchByName(string nome)
        {
            return _context.EmpresasDestinacao
                .Where(e => e.Nome.Contains(nome))
                .ToList();
        }

        public bool ExistsByName(string nome)
        {
            return _context.EmpresasDestinacao
                .Any(e => e.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }
    }
}