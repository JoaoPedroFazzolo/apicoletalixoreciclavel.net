using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;

namespace apicoletalixoreciclavel.Tests.Services
{
    public class RelatorioServiceTests
    {
        private readonly Mock<IRelatorioRepository> _mockRepository;
        private readonly RelatorioService _service;

        public RelatorioServiceTests()
        {
            _mockRepository = new Mock<IRelatorioRepository>();
            _service = new RelatorioService(_mockRepository.Object);
        }

        [Fact]
        public void ObterTodosRelatorios_DeveRetornarTodosRelatorios()
        {
            var relatoriosEsperados = new List<RelatorioModel>
            {
                new RelatorioModel { RelatorioId = 1, Nome = "Relatório A", DataGeracao = DateTime.Now, TipoRelatorio = "Anual" },
                new RelatorioModel { RelatorioId = 2, Nome = "Relatório B", DataGeracao = DateTime.Now, TipoRelatorio = "Mensal" }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(relatoriosEsperados);

            var resultado = _service.ObterTodosRelatorios();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterTodosRelatorios_QuandoNaoHaRelatorios_DeveRetornarListaVazia()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<RelatorioModel>());

            var resultado = _service.ObterTodosRelatorios();

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterRelatorioPorId_ComIdValido_DeveRetornarRelatorio()
        {
            long id = 1;
            var relatorioEsperado = new RelatorioModel { RelatorioId = id, Nome = "Relatório A", DataGeracao = DateTime.Now, TipoRelatorio = "Anual" };
            _mockRepository.Setup(x => x.GetById(id)).Returns(relatorioEsperado);

            var resultado = _service.ObterRelatorioPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.RelatorioId);
            Assert.Equal("Relatório A", resultado.Nome);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterRelatorioPorId_ComIdInexistente_DeveRetornarNull()
        {
            long id = 999;
            _mockRepository.Setup(x => x.GetById(id)).Returns((RelatorioModel)null);

            var resultado = _service.ObterRelatorioPorId(id);

            Assert.Null(resultado);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void CriarRelatorio_ComRelatorioValido_DeveChamarRepositoryAdd()
        {
            var relatorio = new RelatorioModel { RelatorioId = 1, Nome = "Relatório A", TipoRelatorio = "Anual" };

            _service.CriarRelatorio(relatorio);

            _mockRepository.Verify(x => x.Add(relatorio), Times.Once);
            Assert.True(relatorio.DataGeracao != default(DateTime));
        }

        [Fact]
        public void CriarRelatorio_ComNomeVazio_DeveLancarArgumentException()
        {
            var relatorio = new RelatorioModel { RelatorioId = 1, Nome = "", TipoRelatorio = "Anual" };

            var exception = Assert.Throws<ArgumentException>(() => _service.CriarRelatorio(relatorio));

            Assert.Equal("O nome do relatório é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.Add(It.IsAny<RelatorioModel>()), Times.Never);
        }

        [Fact]
        public void CriarRelatorio_ComNomeNull_DeveLancarArgumentException()
        {
            var relatorio = new RelatorioModel { RelatorioId = 1, Nome = null, TipoRelatorio = "Anual" };

            var exception = Assert.Throws<ArgumentException>(() => _service.CriarRelatorio(relatorio));

            Assert.Equal("O nome do relatório é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.Add(It.IsAny<RelatorioModel>()), Times.Never);
        }

        [Fact]
        public void CriarRelatorio_ComRelatorioNull_DeveLancarArgumentException()
        {
            RelatorioModel relatorio = null;

            var exception = Assert.Throws<ArgumentException>(() => _service.CriarRelatorio(relatorio));

            Assert.Equal("O nome do relatório é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.Add(It.IsAny<RelatorioModel>()), Times.Never);
        }

        [Fact]
        public void AtualizarRelatorio_ComRelatorioValido_DeveChamarRepositoryUpdate()
        {
            var relatorio = new RelatorioModel { RelatorioId = 1, Nome = "Relatório Atualizado", TipoRelatorio = "Anual" };

            _service.AtualizarRelatorio(relatorio);

            _mockRepository.Verify(x => x.Update(relatorio), Times.Once);
        }

        [Fact]
        public void AtualizarRelatorio_ComNomeVazio_DeveLancarArgumentException()
        {
            var relatorio = new RelatorioModel { RelatorioId = 1, Nome = "", TipoRelatorio = "Anual" };

            var exception = Assert.Throws<ArgumentException>(() => _service.AtualizarRelatorio(relatorio));

            Assert.Equal("O nome do relatório é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.Update(It.IsAny<RelatorioModel>()), Times.Never);
        }

        [Fact]
        public void AtualizarRelatorio_ComNomeNull_DeveLancarArgumentException()
        {
            var relatorio = new RelatorioModel { RelatorioId = 1, Nome = null, TipoRelatorio = "Anual" };

            var exception = Assert.Throws<ArgumentException>(() => _service.AtualizarRelatorio(relatorio));

            Assert.Equal("O nome do relatório é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.Update(It.IsAny<RelatorioModel>()), Times.Never);
        }

        [Fact]
        public void DeletarRelatorio_ComRelatorioValido_DeveChamarRepositoryDelete()
        {
            var relatorio = new RelatorioModel { RelatorioId = 1, Nome = "Relatório A", TipoRelatorio = "Anual" };

            _service.DeletarRelatorio(relatorio);

            _mockRepository.Verify(x => x.Delete(relatorio), Times.Once);
        }

        [Fact]
        public void DeletarRelatorio_ComRelatorioNull_DeveChamarRepositoryDelete()
        {
            RelatorioModel relatorio = null;

            _service.DeletarRelatorio(relatorio);

            _mockRepository.Verify(x => x.Delete(relatorio), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(999)]
        public void ObterRelatorioPorId_ComDiferentesIds_DeveChamarRepositoryGetById(long id)
        {
            var relatorio = new RelatorioModel { RelatorioId = id, Nome = $"Relatório {id}", TipoRelatorio = "Anual" };
            _mockRepository.Setup(x => x.GetById(id)).Returns(relatorio);

            var resultado = _service.ObterRelatorioPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.RelatorioId);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }
    }
}