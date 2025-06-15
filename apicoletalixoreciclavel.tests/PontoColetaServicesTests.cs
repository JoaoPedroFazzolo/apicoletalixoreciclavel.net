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
    public class PontoColetaServiceTests
    {
        private readonly Mock<IPontoColetaRepository> _mockRepository;
        private readonly PontoColetaService _service;

        public PontoColetaServiceTests()
        {
            _mockRepository = new Mock<IPontoColetaRepository>();
            _service = new PontoColetaService(_mockRepository.Object);
        }

        [Fact]
        public void ObterTodosPontosColetas_DeveRetornarTodosPontosColetas()
        {
            var pontosEsperados = new List<PontoColetaModel>
            {
                new PontoColetaModel { PontoColetaId = 1, Nome = "Ponto A", Endereco = "Rua 1", Capacidade = 100 },
                new PontoColetaModel { PontoColetaId = 2, Nome = "Ponto B", Endereco = "Rua 2", Capacidade = 200 }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(pontosEsperados);

            var resultado = _service.ObterTodosPontosColetas();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterTodosPontosColetas_QuandoNaoHaPontos_DeveRetornarListaVazia()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<PontoColetaModel>());

            var resultado = _service.ObterTodosPontosColetas();

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterTodosPontosColetasComDetalhes_DeveRetornarTodosPontosColetasComDetalhes()
        {
            var pontosEsperados = new List<PontoColetaModel>
            {
                new PontoColetaModel { PontoColetaId = 1, Nome = "Ponto A", Endereco = "Rua 1", Capacidade = 100 },
                new PontoColetaModel { PontoColetaId = 2, Nome = "Ponto B", Endereco = "Rua 2", Capacidade = 200 }
            };

            _mockRepository.Setup(x => x.GetAllWithDetails()).Returns(pontosEsperados);

            var resultado = _service.ObterTodosPontosColetasComDetalhes();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAllWithDetails(), Times.Once);
        }

        [Fact]
        public void ObterPontoColetaPorId_ComIdValido_DeveRetornarPontoColeta()
        {
            long id = 1;
            var pontoEsperado = new PontoColetaModel { PontoColetaId = id, Nome = "Ponto A", Endereco = "Rua 1", Capacidade = 100 };
            _mockRepository.Setup(x => x.GetById(id)).Returns(pontoEsperado);

            var resultado = _service.ObterPontoColetaPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.PontoColetaId);
            Assert.Equal("Ponto A", resultado.Nome);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterPontoColetaPorId_ComIdInexistente_DeveRetornarNull()
        {
            long id = 999;
            _mockRepository.Setup(x => x.GetById(id)).Returns((PontoColetaModel)null);

            var resultado = _service.ObterPontoColetaPorId(id);

            Assert.Null(resultado);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterPontoColetaPorIdComDetalhes_ComIdValido_DeveRetornarPontoColetaComDetalhes()
        {
            long id = 1;
            var pontoEsperado = new PontoColetaModel { PontoColetaId = id, Nome = "Ponto A", Endereco = "Rua 1", Capacidade = 100 };
            _mockRepository.Setup(x => x.GetByIdWithDetails(id)).Returns(pontoEsperado);

            var resultado = _service.ObterPontoColetaPorIdComDetalhes(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.PontoColetaId);
            _mockRepository.Verify(x => x.GetByIdWithDetails(id), Times.Once);
        }

        [Fact]
        public void AdicionarPontoColeta_ComPontoValido_DeveChamarRepositoryAdd()
        {
            var ponto = new PontoColetaModel { PontoColetaId = 1, Nome = "Ponto A", Endereco = "Rua 1", Capacidade = 100 };

            _service.AdicionarPontoColeta(ponto);

            _mockRepository.Verify(x => x.Add(ponto), Times.Once);
        }

        [Fact]
        public void AdicionarPontoColeta_ComPontoNull_DeveChamarRepositoryAdd()
        {
            PontoColetaModel ponto = null;

            _service.AdicionarPontoColeta(ponto);

            _mockRepository.Verify(x => x.Add(ponto), Times.Once);
        }

        [Fact]
        public void AtualizarPontoColeta_ComPontoValido_DeveChamarRepositoryUpdate()
        {
            var ponto = new PontoColetaModel { PontoColetaId = 1, Nome = "Ponto A Atualizado", Endereco = "Rua 1", Capacidade = 150 };

            _service.AtualizarPontoColeta(ponto);

            _mockRepository.Verify(x => x.Update(ponto), Times.Once);
        }

        [Fact]
        public void AtualizarPontoColeta_ComPontoNull_DeveLancarArgumentException()
        {
            PontoColetaModel ponto = null;

            var exception = Assert.Throws<ArgumentException>(() => 
                _service.AtualizarPontoColeta(ponto));
            
            Assert.Equal("PontoColetaModel is null", exception.Message);
        }

        [Fact]
        public void AtualizarPontoColeta_ComPontoNull_NaoDeveChamarRepositoryUpdate()
        {
            PontoColetaModel ponto = null;

            Assert.Throws<ArgumentException>(() => 
                _service.AtualizarPontoColeta(ponto));
            
            _mockRepository.Verify(x => x.Update(It.IsAny<PontoColetaModel>()), Times.Never);
        }

        [Fact]
        public void DeletarPontoColeta_ComIdValido_DeveChamarRepositoryDelete()
        {
            long id = 1;

            _service.DeletarPontoColeta(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(999)]
        public void ObterPontoColetaPorId_ComDiferentesIds_DeveChamarRepositoryGetById(long id)
        {
            var ponto = new PontoColetaModel { PontoColetaId = id, Nome = $"Ponto {id}", Endereco = $"Rua {id}", Capacidade = 100 * id };
            _mockRepository.Setup(x => x.GetById(id)).Returns(ponto);

            var resultado = _service.ObterPontoColetaPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.PontoColetaId);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        [InlineData(long.MaxValue)]
        public void DeletarPontoColeta_ComDiferentesIds_DeveChamarRepositoryDelete(long id)
        {
            _service.DeletarPontoColeta(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }
    }
}