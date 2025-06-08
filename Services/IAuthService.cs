using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Services;

public interface IAuthService
{
    UsuarioModel Authenticate(string username, string password);
}