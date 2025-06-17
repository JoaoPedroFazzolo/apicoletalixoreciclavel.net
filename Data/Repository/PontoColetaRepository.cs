using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Repository;

public class PontoColetaRepository : IPontoColetaRepository
{
    private readonly DatabaseContext _context;

    public PontoColetaRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<PontoColetaModel> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        return _context.PontoColetas
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public IEnumerable<PontoColetaModel> GetAllWithDetails(int pageNumber = 1, int pageSize = 10)
    {
        return _context.PontoColetas
            .Include(p => p.Coletas)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public PontoColetaModel GetById(long id)
    {
        return _context.PontoColetas.Find(id);
    }

    public PontoColetaModel GetByIdWithDetails(long id)
    {
        return _context.PontoColetas
            .Where(p => p.PontoColetaId == id)
            .Include(p => p.Coletas)
            .FirstOrDefault();
    }

    public void Add(PontoColetaModel pontoColetaModel)
    {
        _context.PontoColetas.Add(pontoColetaModel);
        _context.SaveChanges();
    }

    public void Update(PontoColetaModel pontoColetaModel)
    {
        _context.PontoColetas.Update(pontoColetaModel);
        _context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entity = _context.PontoColetas.Find(id);
        if (entity != null)
        {
            _context.PontoColetas.Remove(entity);
            _context.SaveChanges();
        }
    }
}