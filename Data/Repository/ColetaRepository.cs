using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Repository;

public class ColetaRepository : IColetaRepository
{
    private readonly DatabaseContext _context;

    public ColetaRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<ColetaModel> GetAll()
    {
        return _context.Coletas
            .ToList();
    }

    public IEnumerable<ColetaModel> GetAllWithDetails()
    {
        return _context.Coletas
            .Include(p => p.PontoColeta)
            .Include(p => p.Residuo)
            .ToList();
    }

    public ColetaModel GetById(long id)
    {
        return _context.Coletas.Find(id);
    }

    public ColetaModel GetByIdWithDetails(long id)
    {
        return _context.Coletas
            .Where(p => p.ColetaId == id)
            .Include(p => p.PontoColeta)
            .Include(p => p.Residuo)
            .FirstOrDefault();
    }

    public void Add(ColetaModel coletaModel)
    {
        _context.Coletas.Add(coletaModel);
        _context.SaveChanges();
    }

    public void Update(ColetaModel coletaModel)
    {
        _context.Coletas.Update(coletaModel);
        _context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entity = _context.Coletas.Find(id);
        if (entity != null)
        {
            _context.Coletas.Remove(entity);
            _context.SaveChanges();
        }
    }
}