using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace apicoletalixoreciclavel.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> CriarUsuario([FromBody] CreateUsuarioViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (sucesso, erro, usuario) = await _usuarioService.CriarUsuarioAsync(model);

        if (!sucesso)
            return erro == "Já existe um usuário com este e-mail." ? Conflict(erro) : BadRequest(erro);

        return CreatedAtAction(nameof(CriarUsuario), new { id = usuario!.UsuarioId }, new
        {
            usuario.UsuarioId,
            usuario.Nome,
            usuario.Email,
            usuario.Role
        });
    }
}