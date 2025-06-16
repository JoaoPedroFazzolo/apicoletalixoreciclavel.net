using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Data.Repository;

public class ResiduoEletronicoRepository : IResiduoEletronicoRepository
{
    private readonly DatabaseContext _context;

    public ResiduoEletronicoRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<ResiduoEletronicoModel> GetAll()
    {
        return _context.ResiduoEletronicos
            .ToList();
    }

    public IEnumerable<ResiduoEletronicoModel> GetAllWithDetails()
    {
        return _context.ResiduoEletronicos
            .Include(p => p.Tipo)
            .Include(p => p.Marca)
            .Include(p => p.Modelo)
            .Include(p => p.Estado)
            .Include(p => p.Status)
            .Include(p => p.Usuario)
            .ToList();
    }

    public ResiduoEletronicoModel GetById(long id)
    {
       return _context.ResiduoEletronicos.Find(id);
    }

    public ResiduoEletronicoModel GetByIdWithDetails(long usuarioId)
    {
        return _context.ResiduoEletronicos
            .Where(p => p.UsuarioId == usuarioId)
            .Include(p => p.Tipo)
            .Include(p => p.Marca)
            .Include(p => p.Modelo)
            .Include(p => p.Estado)
            .Include(p => p.Status)
            .Include(p => p.Usuario)
            .FirstOrDefault();;
    }

    public void Add(ResiduoEletronicoModel residuoEletronicoModel)
    {
        _context.ResiduoEletronicos.Add(residuoEletronicoModel);
        _context.SaveChanges();
    }

    public void Update(ResiduoEletronicoModel residuoEletronicoModel)
    {
        _context.ResiduoEletronicos.Update(residuoEletronicoModel);
        _context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entity = _context.ResiduoEletronicos.Find(id);
        if (entity != null)
        {
            _context.ResiduoEletronicos.Remove(entity);
            _context.SaveChanges();
        }

    }
}