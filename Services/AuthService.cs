using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apicoletalixoreciclavel.Models;
using Microsoft.IdentityModel.Tokens;

namespace apicoletalixoreciclavel.Services;

public class AuthService: IAuthService
{
    public UsuarioModel Authenticate(string username, string password)
    {
        throw new NotImplementedException();
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