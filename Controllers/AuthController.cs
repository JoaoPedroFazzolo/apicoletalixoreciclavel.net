using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace apicoletalixoreciclavel.Controllers;
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UsuarioModel user)
    {
        var authenticatedUser = _authService.Authenticate(user.Nome, user.Senha);
        if (authenticatedUser == null)
        {
            return Unauthorized();
        }

        var token = _authService.GenerateJwtToken(authenticatedUser);
        return Ok(new { Token = token });
    }
}