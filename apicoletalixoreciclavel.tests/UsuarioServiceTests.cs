using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace apicoletalixoreciclavel.Tests.Services
{
    public class UsuarioServiceTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DatabaseContext(options);
            _usuarioService = new UsuarioService(_context);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComDadosValidos_DeveRetornarSucesso()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "João Silva",
                Email = "joao@teste.com",
                Senha = "123456",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.True(resultado.Sucesso);
            Assert.Null(resultado.Erro);
            Assert.NotNull(resultado.Usuario);
            Assert.Equal(model.Nome, resultado.Usuario.Nome);
            Assert.Equal(model.Email, resultado.Usuario.Email);
            Assert.Equal(model.Role, resultado.Usuario.Role);
            Assert.True(BCrypt.Net.BCrypt.Verify(model.Senha, resultado.Usuario.Senha));
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComNomeVazio_DeveRetornarErro()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "",
                Email = "joao@teste.com",
                Senha = "123456",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Nome, e-mail e senha são obrigatórios.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComNomeNull_DeveRetornarErro()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = null,
                Email = "joao@teste.com",
                Senha = "123456",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Nome, e-mail e senha são obrigatórios.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComNomeApenasEspacos_DeveRetornarErro()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "   ",
                Email = "joao@teste.com",
                Senha = "123456",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Nome, e-mail e senha são obrigatórios.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComEmailVazio_DeveRetornarErro()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "João Silva",
                Email = "",
                Senha = "123456",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Nome, e-mail e senha são obrigatórios.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComEmailNull_DeveRetornarErro()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "João Silva",
                Email = null,
                Senha = "123456",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Nome, e-mail e senha são obrigatórios.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComSenhaVazia_DeveRetornarErro()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "João Silva",
                Email = "joao@teste.com",
                Senha = "",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Nome, e-mail e senha são obrigatórios.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComSenhaNull_DeveRetornarErro()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "João Silva",
                Email = "joao@teste.com",
                Senha = null,
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Nome, e-mail e senha são obrigatórios.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComEmailExistente_DeveRetornarErro()
        {
            // Arrange
            var usuarioExistente = new UsuarioModel
            {
                Nome = "Maria Silva",
                Email = "maria@teste.com",
                Senha = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "User"
            };

            await _context.Usuarios.AddAsync(usuarioExistente);
            await _context.SaveChangesAsync();

            var model = new UsuarioCreateViewModel
            {
                Nome = "João Silva",
                Email = "maria@teste.com",
                Senha = "654321",
                Role = "Admin"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Já existe um usuário com este e-mail.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_DeveSalvarUsuarioNoBancoDeDados()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "Carlos Silva",
                Email = "carlos@teste.com",
                Senha = "123456",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            var usuarioSalvo = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            Assert.NotNull(usuarioSalvo);
            Assert.Equal(model.Nome, usuarioSalvo.Nome);
            Assert.Equal(model.Email, usuarioSalvo.Email);
            Assert.Equal(model.Role, usuarioSalvo.Role);
            Assert.True(BCrypt.Net.BCrypt.Verify(model.Senha, usuarioSalvo.Senha));
        }

        [Fact]
        public async Task CriarUsuarioAsync_DeveCriptografarSenha()
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "Ana Silva",
                Email = "ana@teste.com",
                Senha = "minhasenha123",
                Role = "User"
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.True(resultado.Sucesso);
            Assert.NotEqual(model.Senha, resultado.Usuario.Senha);
            Assert.True(BCrypt.Net.BCrypt.Verify(model.Senha, resultado.Usuario.Senha));
        }

        [Theory]
        [InlineData("User")]
        [InlineData("Admin")]
        [InlineData("Manager")]
        public async Task CriarUsuarioAsync_ComDiferentesRoles_DeveRetornarSucesso(string role)
        {
            // Arrange
            var model = new UsuarioCreateViewModel
            {
                Nome = "Teste Role",
                Email = $"teste{role.ToLower()}@teste.com",
                Senha = "123456",
                Role = role
            };

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.True(resultado.Sucesso);
            Assert.Equal(role, resultado.Usuario.Role);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComEmailDiferenteCase_DevePermitirCriacao()
        {
            // Arrange
            var usuario1 = new UsuarioCreateViewModel
            {
                Nome = "Usuário 1",
                Email = "teste@exemplo.com",
                Senha = "123456",
                Role = "User"
            };
            var usuario2 = new UsuarioCreateViewModel
            {
                Nome = "Usuário 2",
                Email = "TESTE@EXEMPLO.COM",
                Senha = "654321",
                Role = "User"
            };

            // Act
            var resultado1 = await _usuarioService.CriarUsuarioAsync(usuario1);
            var resultado2 = await _usuarioService.CriarUsuarioAsync(usuario2);

            // Assert
            Assert.True(resultado1.Sucesso);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}