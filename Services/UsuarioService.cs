using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DatabaseContext _context;

        public UsuarioService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<(bool Sucesso, string? Erro, UsuarioModel? Usuario)> CriarUsuarioAsync(UsuarioCreateViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Nome) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Senha))
            {
                return (false, "Nome, e-mail e senha são obrigatórios.", null);
            }

            var usuarioExistente = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == model.Email);

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

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return (true, null, novoUsuario);
        }
    }
}