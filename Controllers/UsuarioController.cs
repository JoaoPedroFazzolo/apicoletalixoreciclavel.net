using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        
        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioService.ListarUsuariosAsync();
            return Ok(usuarios);
        }
        
        [HttpGet("{id}")]
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