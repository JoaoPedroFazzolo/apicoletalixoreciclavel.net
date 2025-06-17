using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public interface IUsuarioRepository
    {
        Task<UsuarioModel?> GetByEmailAsync(string email);
        Task<UsuarioModel> CreateAsync(UsuarioModel usuario);
        Task<UsuarioModel?> GetByIdAsync(long id);
        Task<IReadOnlyList<UsuarioModel>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task UpdateAsync(UsuarioModel usuario);
        Task DeleteAsync(long id);
        
        
    }
}