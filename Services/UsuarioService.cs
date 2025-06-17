using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Telefone = model.Telefone,
                Endereco = model.Endereco,
                Cep = model.Cep,
                Cidade = model.Cidade,
                Estado = model.Estado,
                Role = model.Role
            };

            var usuarioCriado = await _usuarioRepository.CreateAsync(novoUsuario);

            return (true, null, usuarioCriado);
        }

        public async Task<IReadOnlyList<UsuarioViewModel>> ListarUsuariosAsync(int pageNumber = 1, int pageSize = 10)
        {
            var usuarios = await _usuarioRepository.GetAllAsync(pageNumber, pageSize);
            return usuarios.Select(u => new UsuarioViewModel
            {
                UsuarioId = u.UsuarioId,
                Nome = u.Nome,
                Email = u.Email,
                Role = u.Role ?? string.Empty
            }).ToList().AsReadOnly();
        }

        public async Task<UsuarioViewModel?> ObterUsuarioPorIdAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return null;
            }

            return new UsuarioViewModel
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Role = usuario.Role ?? string.Empty
            };
        }

        public async Task<(bool Sucesso, string? Erro)> AtualizarUsuarioAsync(long id, UpdateUsuarioViewModel model)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return (false, "Usuário não encontrado.");
            }

            if (!string.IsNullOrWhiteSpace(model.Nome))
                usuario.Nome = model.Nome;
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                var usuarioExistente = await _usuarioRepository.GetByEmailAsync(model.Email);
                if (usuarioExistente != null && usuarioExistente.UsuarioId != id)
                {
                    return (false, "Já existe um usuário com este e-mail.");
                }
                usuario.Email = model.Email;
            }
            if (!string.IsNullOrWhiteSpace(model.Senha))
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha);
            if (!string.IsNullOrWhiteSpace(model.Telefone))
                usuario.Telefone = model.Telefone;
            if (!string.IsNullOrWhiteSpace(model.Endereco))
                usuario.Endereco = model.Endereco;
            if (!string.IsNullOrWhiteSpace(model.Cep))
                usuario.Cep = model.Cep;
            if (!string.IsNullOrWhiteSpace(model.Cidade))
                usuario.Cidade = model.Cidade;
            if (!string.IsNullOrWhiteSpace(model.Estado))
                usuario.Estado = model.Estado;
            if (!string.IsNullOrWhiteSpace(model.Role))
                usuario.Role = model.Role;

            await _usuarioRepository.UpdateAsync(usuario);
            return (true, null);
        }

        public async Task<(bool Sucesso, string? Erro)> DeletarUsuarioAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return (false, "Usuário não encontrado.");
            }

            await _usuarioRepository.DeleteAsync(id);
            return (true, null);
        }
    }
}