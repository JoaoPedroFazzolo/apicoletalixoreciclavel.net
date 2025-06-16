using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using Microsoft.EntityFrameworkCore;

namespace apicoletalixoreciclavel.Tests.Services;

public class AuthServiceTests : IDisposable
{
    private readonly DatabaseContext _context;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        // Configurar banco em memória para testes
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DatabaseContext(options);
        _authService = new AuthService(_context);
    }

    [Fact]
    public async Task Authenticate_UsuarioExisteComSenhaCorreta_DeveRetornarUsuario()
    {
        // Arrange
        var email = "teste@teste.com";
        var senha = "123456";
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
        
        var usuario = new UsuarioModel
        {
            UsuarioId = 1,
            Nome = "Usuário Teste",
            Email = email,
            Senha = senhaHash,
            Telefone = "123456",
            Endereco = "Rua teste",
            Cep = "123-000",
            Cidade = "São Paulo",
            Estado = "SP",
            Role = "User"
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _authService.Authenticate(email, senha);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(usuario.UsuarioId, resultado.UsuarioId);
        Assert.Equal(usuario.Email, resultado.Email);
        Assert.Equal(usuario.Nome, resultado.Nome);
    }

    [Fact]
    public async Task Authenticate_UsuarioNaoExiste_DeveRetornarNull()
    {
        // Arrange
        var email = "inexistente@teste.com";
        var senha = "123456";

        // Act
        var resultado = await _authService.Authenticate(email, senha);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public async Task Authenticate_UsuarioExisteComSenhaIncorreta_DeveRetornarNull()
    {
        // Arrange
        var email = "teste@teste.com";
        var senhaCorreta = "123456";
        var senhaIncorreta = "senha_errada";
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(senhaCorreta);
        
        var usuario = new UsuarioModel
        {
            UsuarioId = 1,
            Nome = "Usuário Teste",
            Email = email,
            Senha = senhaHash,
            Telefone = "123456",
            Endereco = "Rua teste",
            Cep = "123-000",
            Cidade = "São Paulo",
            Estado = "SP",
            Role = "User"
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _authService.Authenticate(email, senhaIncorreta);

        // Assert
        Assert.Null(resultado);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Authenticate_EmailInvalido_DeveRetornarNull(string emailInvalido)
    {
        // Arrange & Act
        var resultado = await _authService.Authenticate(emailInvalido, "123456");

        // Assert
        Assert.Null(resultado);
    }
    

    [Fact]
    public void GenerateJwtToken_UsuarioValido_DeveRetornarTokenValido()
    {
        // Arrange
        var usuario = new UsuarioModel
        {
            UsuarioId = 1,
            Nome = "Usuário Teste",
            Email = "teste@teste.com",
            Role = "Admin"
        };

        // Act
        var token = _authService.GenerateJwtToken(usuario);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        Assert.True(tokenHandler.CanReadToken(token));
    }

    [Fact]
    public void GenerateJwtToken_UsuarioValido_DeveConterClaimsCorretos()
    {
        // Arrange
        var usuario = new UsuarioModel
        {
            UsuarioId = 1,
            Nome = "João Silva",
            Email = "joao@teste.com",
            Role = "Manager"
        };

        // Act
        var token = _authService.GenerateJwtToken(usuario);

        // Assert
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        
        Assert.NotNull(jwtToken);
        Assert.True(jwtToken.Claims.Any(), "Token deve conter pelo menos um claim");
        
        var allClaims = jwtToken.Claims.ToList();
        Console.WriteLine($"Claims encontrados: {string.Join(", ", allClaims.Select(c => $"{c.Type}:{c.Value}"))}");
        
        Assert.NotNull(jwtToken.Header);
        Assert.NotNull(jwtToken.Payload);
    }

    [Fact]
    public void GenerateJwtToken_UsuarioValido_DeveDefinirExpiracaoCorreta()
    {
        // Arrange
        var usuario = new UsuarioModel
        {
            UsuarioId = 1,
            Nome = "Usuário Teste",
            Email = "teste@teste.com",
            Role = "User"
        };

        var agora = DateTime.UtcNow;

        // Act
        var token = _authService.GenerateJwtToken(usuario);

        // Assert
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        
        var expiracao = jwtToken.ValidTo;
        var diferencaEsperada = TimeSpan.FromMinutes(5);
        var diferencaReal = expiracao - agora;
        
        Assert.True(Math.Abs((diferencaReal - diferencaEsperada).TotalSeconds) < 10);
    }

    [Fact]
    public void GenerateJwtToken_UsuarioValido_DeveDefinirIssuerCorreto()
    {
        // Arrange
        var usuario = new UsuarioModel
        {
            UsuarioId = 1,
            Nome = "Usuário Teste",
            Email = "teste@teste.com",
            Role = "User"
        };

        // Act
        var token = _authService.GenerateJwtToken(usuario);

        // Assert
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        Assert.Equal("fiap", jwtToken.Issuer);
    }

    [Fact]
    public void GenerateJwtToken_DoisTokensParaMesmoUsuario_DevemSerDiferentes()
    {
        // Arrange
        var usuario = new UsuarioModel
        {
            UsuarioId = 1,
            Nome = "Usuário Teste",
            Email = "teste@teste.com",
            Role = "User"
        };

        // Act
        var token1 = _authService.GenerateJwtToken(usuario);
        System.Threading.Thread.Sleep(1000);
        var token2 = _authService.GenerateJwtToken(usuario);
        
        Assert.NotEqual(token1, token2);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}