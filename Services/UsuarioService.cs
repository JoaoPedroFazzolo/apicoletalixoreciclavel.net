using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.ViewModels;

namespace apicoletalixoreciclavel.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(bool Sucesso, string? Erro, UsuarioModel? Usuario)> CriarUsuarioAsync(CreateUsuarioViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nome) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Senha))
            {
                return (false, "Nome, e-mail e senha são obrigatórios.", null);
            }

            var usuarioExistente = await _usuarioRepository.GetByEmailAsync(model.Email);

            if (usuarioExistente != null)
            {
                return (false, "Já existe um usuário com este e-mail.", null);
            }

            var novoUsuario = new UsuarioModel
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha),
                Role = model.Role
            };

            var usuarioCriado = await _usuarioRepository.CreateAsync(novoUsuario);

            return (true, null, usuarioCriado);
        }
    }
}