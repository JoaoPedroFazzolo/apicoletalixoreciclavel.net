using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace apicoletalixoreciclavel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        
        [HttpPost("registro")]
        [AllowAnonymous]
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioService.ListarUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ObterUsuarioPorId(long id)
        {
            var usuario = await _usuarioService.ObterUsuarioPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> AtualizarUsuario(long id, [FromBody] UpdateUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (sucesso, erro) = await _usuarioService.AtualizarUsuarioAsync(id, model);
            if (!sucesso)
            {
                return BadRequest(erro);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletarUsuario(long id)
        {
            var (sucesso, erro) = await _usuarioService.DeletarUsuarioAsync(id);
            if (!sucesso)
            {
                return BadRequest(erro);
            }

            return NoContent();
        }
    }
}