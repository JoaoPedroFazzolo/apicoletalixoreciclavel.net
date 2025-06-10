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
    private readonly AuthService _authService;

    public AuthController()
    {
        _authService = new AuthService();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UsuarioModel user)
    {
        var authenticatedUser = _authService.Authenticate(user.Nome, user.Senha);
        if (authenticatedUser == null)
        {
            return Unauthorized();
        }

        var token = GenerateJwtToken(authenticatedUser);
        return Ok(new { Token = token });
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
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}