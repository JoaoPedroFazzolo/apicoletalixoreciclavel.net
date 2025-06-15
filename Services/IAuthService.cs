using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public interface IAuthService
{
    Task<UsuarioModel?> Authenticate(string email, string password);
    public string GenerateJwtToken(UsuarioModel user);
}