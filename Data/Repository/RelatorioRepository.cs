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
    
    public IEnumerable<RelatorioModel> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        return _context.Relatorios
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public RelatorioModel GetById(long id)
    {
        return _context.Relatorios.Find(id);
    }

    public void Add(RelatorioModel relatorio)
    {
        _context.Relatorios.Add(relatorio);
        _context.SaveChanges();
    }

    public void Update(RelatorioModel relatorio)
    {
        _context.Relatorios.Update(relatorio);
        _context.SaveChanges();
    }

    public void Delete(RelatorioModel relatorio)
    {
        _context.Relatorios.Remove(relatorio);
        _context.SaveChanges();
    }
}