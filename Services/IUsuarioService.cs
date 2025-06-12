using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.ViewModels;

namespace apicoletalixoreciclavel.Services
{
    public interface IUsuarioService
    {
        Task<(bool Sucesso, string? Erro, UsuarioModel? Usuario)> CriarUsuarioAsync(UsuarioCreateViewModel model);
    }
}
