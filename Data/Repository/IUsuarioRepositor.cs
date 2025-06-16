using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Data.Repository
{
    public interface IUsuarioRepository
    {
        Task<UsuarioModel?> GetByEmailAsync(string email);
        Task<UsuarioModel> CreateAsync(UsuarioModel usuario);
        Task<UsuarioModel?> GetByIdAsync(long id);
    }
}