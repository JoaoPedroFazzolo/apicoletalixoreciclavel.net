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
        return _context.Relatorios.ToList(); // Mudei de Relatorio para Relatorios
    }

    public RelatorioModel GetById(long id)
    {
        return _context.Relatorios.Find(id); // Mudei de Relatorio para Relatorios
    }

    public void Add(RelatorioModel relatorio)
    {
        _context.Relatorios.Add(relatorio); // Mudei de Relatorio para Relatorios
        _context.SaveChanges();
    }

    public void Update(RelatorioModel relatorio)
    {
        _context.Relatorios.Update(relatorio); // Mudei de Relatorio para Relatorios
        _context.SaveChanges();
    }

    public void Delete(RelatorioModel relatorio)
    {
        _context.Relatorios.Remove(relatorio); // Mudei de Relatorio para Relatorios
        _context.SaveChanges();
    }
}