using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace apicoletalixoreciclavel.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // CORRIGIDO: Injeção de dependência via construtor
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Autentica um usuário e retorna um token JWT
    /// </summary>
    /// <param name="user">Dados do usuário para autenticação</param>
    /// <returns>Token JWT para acesso à API</returns>
    /// <response code="200">Autenticação realizada com sucesso</response>
    /// <response code="401">Credenciais inválidas</response>
    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public IActionResult Login([FromBody] UsuarioModel user)
    {
        var authenticatedUser = _authService.Authenticate(user.Nome, user.Senha);
        if (authenticatedUser == null)
        {
            return Unauthorized(new { message = "Credenciais inválidas" });
        }

        var token = GenerateJwtToken(authenticatedUser);
        return Ok(new { Token = token, ExpiresIn = 300 }); // 5 minutos
    }

    private string GenerateJwtToken(UsuarioModel user)
    {
        byte[] secret = Encoding.ASCII.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi");
        var securityKey = new SymmetricSecurityKey(secret);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = "fiap",
            SigningCredentials = credentials
        };

        SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(securityToken);
    }
}