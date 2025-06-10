using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly DatabaseContext _context;

    public UsuarioController(DatabaseContext context)
    {
        _context = context;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> CriarUsuario([FromBody] UsuarioCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(model.Nome) ||
            string.IsNullOrWhiteSpace(model.Email) ||
            string.IsNullOrWhiteSpace(model.Senha))
        {
            return BadRequest("Nome, e-mail e senha são obrigatórios.");
        }

        var usuarioExistente = await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == model.Email);

        if (usuarioExistente != null)
            return Conflict("Já existe um usuário com este e-mail.");

        var novoUsuario = new UsuarioModel
        {
            Nome = model.Nome,
            Email = model.Email,
            Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha),
            Role = "User"
        };

        _context.Usuarios.Add(novoUsuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CriarUsuario), new { id = novoUsuario.UsuarioId }, new
        {
            novoUsuario.UsuarioId,
            novoUsuario.Nome,
            novoUsuario.Email,
            novoUsuario.Role
        });
    }
}