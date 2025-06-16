using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace apicoletalixoreciclavel.Services;

public class AuthService: IAuthService
{
    private readonly DatabaseContext _context;

    public AuthService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<UsuarioModel?> Authenticate(string email, string password)
    {
        var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return null;

        bool senhaValida = BCrypt.Net.BCrypt.Verify(password, user.Senha);

        return senhaValida ? user : null;
    }
    public string GenerateJwtToken(UsuarioModel user)
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