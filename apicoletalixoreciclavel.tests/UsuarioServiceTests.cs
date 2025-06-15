using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Moq;
using Xunit;

namespace apicoletalixoreciclavel.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockRepository;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _mockRepository = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_mockRepository.Object);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComDadosValidos_DeveRetornarSucesso()
        {
            // Arrange
            var model = new CreateUsuarioViewModel
            {
                Nome = "João Silva",
                Email = "joao@teste.com",
                Senha = "123456",
                Role = "User"
            };

            _mockRepository.Setup(r => r.GetByEmailAsync(model.Email))
                          .ReturnsAsync((UsuarioModel?)null);
            
            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<UsuarioModel>()))
                          .ReturnsAsync((UsuarioModel u) => u);

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
            var model = new CreateUsuarioViewModel
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
        public async Task CriarUsuarioAsync_ComEmailVazio_DeveRetornarErro()
        {
            // Arrange
            var model = new CreateUsuarioViewModel
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
        public async Task CriarUsuarioAsync_ComSenhaVazia_DeveRetornarErro()
        {
            // Arrange
            var model = new CreateUsuarioViewModel
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
        public async Task CriarUsuarioAsync_ComEmailExistente_DeveRetornarErro()
        {
            // Arrange
            var usuarioExistente = new UsuarioModel
            {
                UsuarioId = 1,
                Nome = "Maria Silva",
                Email = "maria@teste.com",
                Senha = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "User"
            };

            var model = new CreateUsuarioViewModel
            {
                Nome = "João Silva",
                Email = "maria@teste.com",
                Senha = "654321",
                Role = "Admin"
            };

            _mockRepository.Setup(r => r.GetByEmailAsync(model.Email))
                          .ReturnsAsync(usuarioExistente);

            // Act
            var resultado = await _usuarioService.CriarUsuarioAsync(model);

            // Assert
            Assert.False(resultado.Sucesso);
            Assert.Equal("Já existe um usuário com este e-mail.", resultado.Erro);
            Assert.Null(resultado.Usuario);
        }

        [Fact]
        public async Task CriarUsuarioAsync_ComNomeNull_DeveRetornarErro()
        {
            // Arrange
            var model = new CreateUsuarioViewModel
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
        public async Task CriarUsuarioAsync_ComEmailNull_DeveRetornarErro()
        {
            // Arrange
            var model = new CreateUsuarioViewModel
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
        public async Task CriarUsuarioAsync_ComSenhaNull_DeveRetornarErro()
        {
            // Arrange
            var model = new CreateUsuarioViewModel
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
    }
}