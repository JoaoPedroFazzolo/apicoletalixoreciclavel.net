using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.ViewModels;

namespace apicoletalixoreciclavel.Services
{
    public interface IUsuarioService
    {
        Task<(bool Sucesso, string? Erro, UsuarioModel? Usuario)> CriarUsuarioAsync(CreateUsuarioViewModel model);
        Task<IReadOnlyList<UsuarioViewModel>> ListarUsuariosAsync(int pageNumber = 1, int pageSize = 10);

        Task<UsuarioViewModel?> ObterUsuarioPorIdAsync(long id);

        Task<(bool Sucesso, string? Erro)> AtualizarUsuarioAsync(long id, UpdateUsuarioViewModel model);

        Task<(bool Sucesso, string? Erro)> DeletarUsuarioAsync(long id);
    }
}