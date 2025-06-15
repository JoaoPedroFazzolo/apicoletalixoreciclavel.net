using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Repository;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly DatabaseContext _context;

    public RelatorioRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<RelatorioModel> GetAll()
    {
        return _context.Relatorio.ToList();
    }

    public RelatorioModel GetById(long id)
    {
        return _context.Relatorio.Find(id);
    }

    public void Add(RelatorioModel relatorio)
    {
        _context.Relatorio.Add(relatorio);
        _context.SaveChanges();
    }

    public void Update(RelatorioModel relatorio)
    {
        _context.Relatorio.Update(relatorio);
        _context.SaveChanges();
    }

    public void Delete(RelatorioModel relatorio)
    {
        _context.Relatorio.Remove(relatorio);
        _context.SaveChanges();
    }
}