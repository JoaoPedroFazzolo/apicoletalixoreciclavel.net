using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using Moq;
using Xunit;

namespace apicoletalixoreciclavel.Tests.Services
{
    public class EmpresaDestinacaoServiceTests
    {
        private readonly Mock<IEmpresaDestinacaoRepository> _mockRepository;
        private readonly EmpresaDestinacaoService _service;

        public EmpresaDestinacaoServiceTests()
        {
            _mockRepository = new Mock<IEmpresaDestinacaoRepository>();
            _service = new EmpresaDestinacaoService(_mockRepository.Object);
        }

        [Fact]
        public void ObterTodasEmpresas_DeveRetornarListaDeEmpresas()
        {
            // Arrange
            var empresas = new List<EmpresaDestinacaoModel>
            {
                new EmpresaDestinacaoModel { EmpresaDestinacaoId = 1, Nome = "Empresa A", Endereco = "Rua A, 123" },
                new EmpresaDestinacaoModel { EmpresaDestinacaoId = 2, Nome = "Empresa B", Endereco = "Rua B, 456" }
            };
            _mockRepository.Setup(x => x.GetAll()).Returns(empresas);

            // Act
            var resultado = _service.ObterTodasEmpresas();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterEmpresaPorId_ComIdValido_DeveRetornarEmpresa()
        {
            // Arrange
            var empresaId = 1L;
            var empresa = new EmpresaDestinacaoModel 
            { 
                EmpresaDestinacaoId = empresaId, 
                Nome = "Empresa Teste", 
                Endereco = "Rua Teste, 123" 
            };
            _mockRepository.Setup(x => x.GetById(empresaId)).Returns(empresa);

            // Act
            var resultado = _service.ObterEmpresaPorId(empresaId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(empresaId, resultado.EmpresaDestinacaoId);
            Assert.Equal("Empresa Teste", resultado.Nome);
            _mockRepository.Verify(x => x.GetById(empresaId), Times.Once);
        }

        [Fact]
        public void AdicionarEmpresa_ComDadosValidos_DeveAdicionarEmpresa()
        {
            // Arrange
            var empresa = new EmpresaDestinacaoModel 
            { 
                Nome = "Nova Empresa", 
                Endereco = "Rua Nova, 789" 
            };
            _mockRepository.Setup(x => x.ExistsByName(empresa.Nome)).Returns(false);

            // Act
            _service.AdicionarEmpresa(empresa);

            // Assert
            _mockRepository.Verify(x => x.ExistsByName(empresa.Nome), Times.Once);
            _mockRepository.Verify(x => x.Add(empresa), Times.Once);
        }

        [Fact]
        public void AdicionarEmpresa_ComNomeVazio_DeveLancarExcecao()
        {
            // Arrange
            var empresa = new EmpresaDestinacaoModel 
            { 
                Nome = "", 
                Endereco = "Rua Teste, 123" 
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _service.AdicionarEmpresa(empresa));
            Assert.Equal("O nome da empresa é obrigatório.", exception.Message);
        }

        [Fact]
        public void AdicionarEmpresa_ComEnderecoVazio_DeveLancarExcecao()
        {
            // Arrange
            var empresa = new EmpresaDestinacaoModel 
            { 
                Nome = "Empresa Teste", 
                Endereco = "" 
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _service.AdicionarEmpresa(empresa));
            Assert.Equal("O endereço da empresa é obrigatório.", exception.Message);
        }

        [Fact]
        public void AdicionarEmpresa_ComNomeExistente_DeveLancarExcecao()
        {
            // Arrange
            var empresa = new EmpresaDestinacaoModel 
            { 
                Nome = "Empresa Existente", 
                Endereco = "Rua Teste, 123" 
            };
            _mockRepository.Setup(x => x.ExistsByName(empresa.Nome)).Returns(true);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _service.AdicionarEmpresa(empresa));
            Assert.Equal("Já existe uma empresa com este nome.", exception.Message);
        }

        [Fact]
        public void AtualizarEmpresa_ComDadosValidos_DeveAtualizarEmpresa()
        {
            // Arrange
            var empresa = new EmpresaDestinacaoModel 
            { 
                EmpresaDestinacaoId = 1, 
                Nome = "Empresa Atualizada", 
                Endereco = "Rua Atualizada, 123" 
            };
            _mockRepository.Setup(x => x.GetById(empresa.EmpresaDestinacaoId)).Returns(empresa);
            _mockRepository.Setup(x => x.SearchByName(empresa.Nome)).Returns(new List<EmpresaDestinacaoModel>());

            // Act
            _service.AtualizarEmpresa(empresa);

            // Assert
            _mockRepository.Verify(x => x.GetById(empresa.EmpresaDestinacaoId), Times.Once);
            _mockRepository.Verify(x => x.Update(empresa), Times.Once);
        }

        [Fact]
        public void DeletarEmpresa_ComIdValido_DeveDeletarEmpresa()
        {
            // Arrange
            var empresaId = 1L;
            var empresa = new EmpresaDestinacaoModel 
            { 
                EmpresaDestinacaoId = empresaId, 
                Nome = "Empresa para Deletar", 
                Endereco = "Rua Teste, 123" 
            };
            _mockRepository.Setup(x => x.GetById(empresaId)).Returns(empresa);

            // Act
            _service.DeletarEmpresa(empresaId);

            // Assert
            _mockRepository.Verify(x => x.GetById(empresaId), Times.Once);
            _mockRepository.Verify(x => x.Delete(empresaId), Times.Once);
        }

        [Fact]
        public void DeletarEmpresa_ComIdInvalido_DeveLancarExcecao()
        {
            // Arrange
            var empresaId = 999L;
            _mockRepository.Setup(x => x.GetById(empresaId)).Returns((EmpresaDestinacaoModel)null);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _service.DeletarEmpresa(empresaId));
            Assert.Equal("Empresa não encontrada.", exception.Message);
        }

        [Fact]
        public void PesquisarPorNome_ComNomeValido_DeveRetornarEmpresas()
        {
            // Arrange
            var nome = "Empresa";
            var empresas = new List<EmpresaDestinacaoModel>
            {
                new EmpresaDestinacaoModel { EmpresaDestinacaoId = 1, Nome = "Empresa A", Endereco = "Rua A, 123" },
                new EmpresaDestinacaoModel { EmpresaDestinacaoId = 2, Nome = "Empresa B", Endereco = "Rua B, 456" }
            };
            _mockRepository.Setup(x => x.SearchByName(nome)).Returns(empresas);

            // Act
            var resultado = _service.PesquisarPorNome(nome);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.SearchByName(nome), Times.Once);
        }

        [Fact]
        public void PesquisarPorNome_ComNomeVazio_DeveLancarExcecao()
        {
            // Arrange
            var nome = "";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _service.PesquisarPorNome(nome));
            Assert.Equal("O nome para pesquisa é obrigatório.", exception.Message);
        }
    }
}