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
    public class ColetaServiceTests
    {
        private readonly Mock<IColetaRepository> _mockRepository;
        private readonly ColetaService _service;

        public ColetaServiceTests()
        {
            _mockRepository = new Mock<IColetaRepository>();
            _service = new ColetaService(_mockRepository.Object);
        }

        [Fact]
        public void ObterTodasColetas_DeveRetornarTodasColetas()
        {
            // Arrange
            var coletasEsperadas = new List<ColetaModel>
            {
                new ColetaModel { ColetaId = 1, DataColeta = DateTime.Now, ResiduoId = 1, PontoColetaId = 1 },
                new ColetaModel { ColetaId = 2, DataColeta = DateTime.Now, ResiduoId = 2, PontoColetaId = 1 }
            };

            _mockRepository.Setup(x => x.GetAll()).Returns(coletasEsperadas);

            // Act
            var resultado = _service.ObterTodasColetas();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterTodasColetas_QuandoNaoHaColetas_DeveRetornarListaVazia()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<ColetaModel>());

            // Act
            var resultado = _service.ObterTodasColetas();

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterTodasColetasComDetalhes_DeveRetornarTodasColetasComDetalhes()
        {
            // Arrange
            var coletasEsperadas = new List<ColetaModel>
            {
                new ColetaModel { ColetaId = 1, DataColeta = DateTime.Now, ResiduoId = 1, PontoColetaId = 1 },
                new ColetaModel { ColetaId = 2, DataColeta = DateTime.Now, ResiduoId = 2, PontoColetaId = 1 }
            };

            _mockRepository.Setup(x => x.GetAllWithDetails()).Returns(coletasEsperadas);

            // Act
            var resultado = _service.ObterTodasColetasComDetalhes();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAllWithDetails(), Times.Once);
        }

        [Fact]
        public void ObterColetaPorId_ComIdValido_DeveRetornarColeta()
        {
            // Arrange
            long id = 1;
            var coletaEsperada = new ColetaModel { ColetaId = id, DataColeta = DateTime.Now, ResiduoId = 1, PontoColetaId = 1 };
            _mockRepository.Setup(x => x.GetById(id)).Returns(coletaEsperada);

            // Act
            var resultado = _service.ObterColetaPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.ColetaId);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterColetaPorId_ComIdInexistente_DeveRetornarNull()
        {
            // Arrange
            long id = 999;
            _mockRepository.Setup(x => x.GetById(id)).Returns((ColetaModel)null);

            // Act
            var resultado = _service.ObterColetaPorId(id);

            // Assert
            Assert.Null(resultado);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterColetaPorIdComDetalhes_ComIdValido_DeveRetornarColetaComDetalhes()
        {
            // Arrange
            long id = 1;
            var coletaEsperada = new ColetaModel { ColetaId = id, DataColeta = DateTime.Now, ResiduoId = 1, PontoColetaId = 1 };
            _mockRepository.Setup(x => x.GetByIdWithDetails(id)).Returns(coletaEsperada);

            // Act
            var resultado = _service.ObterColetaPorIdComDetalhes(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.ColetaId);
            _mockRepository.Verify(x => x.GetByIdWithDetails(id), Times.Once);
        }

        [Fact]
        public void AdicionarColeta_ComColetaValida_DeveChamarRepositoryAdd()
        {
            // Arrange
            var coleta = new ColetaModel { ColetaId = 1, DataColeta = DateTime.Now, ResiduoId = 1, PontoColetaId = 1 };

            // Act
            _service.AdicionarColeta(coleta);

            // Assert
            _mockRepository.Verify(x => x.Add(coleta), Times.Once);
        }

        [Fact]
        public void AdicionarColeta_ComColetaNull_DeveChamarRepositoryAdd()
        {
            // Arrange
            ColetaModel coleta = null;

            // Act
            _service.AdicionarColeta(coleta);

            // Assert
            _mockRepository.Verify(x => x.Add(coleta), Times.Once);
        }

        [Fact]
        public void AtualizarColeta_ComColetaValida_DeveChamarRepositoryUpdate()
        {
            var coleta = new ColetaModel { ColetaId = 1, DataColeta = DateTime.Now, ResiduoId = 2, PontoColetaId = 1 };

            _service.AtualizarColeta(coleta);

            _mockRepository.Verify(x => x.Update(coleta), Times.Once);
        }

        [Fact]
        public void AtualizarColeta_ComColetaNull_DeveLancarArgumentException()
        {
            ColetaModel coleta = null;

            var exception = Assert.Throws<ArgumentException>(() => 
                _service.AtualizarColeta(coleta));
            
            Assert.Equal("ColetaModel is null", exception.Message);
        }

        [Fact]
        public void AtualizarColeta_ComColetaNull_NaoDeveChamarRepositoryUpdate()
        {
            ColetaModel coleta = null;

            Assert.Throws<ArgumentException>(() => 
                _service.AtualizarColeta(coleta));
            
            _mockRepository.Verify(x => x.Update(It.IsAny<ColetaModel>()), Times.Never);
        }

        [Fact]
        public void DeletarColeta_ComIdValido_DeveChamarRepositoryDelete()
        {
            long id = 1;
            
            _service.DeletarColeta(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(999)]
        public void ObterColetaPorId_ComDiferentesIds_DeveChamarRepositoryGetById(long id)
        {
            var coleta = new ColetaModel { ColetaId = id, DataColeta = DateTime.Now, ResiduoId = id, PontoColetaId = 1 };
            _mockRepository.Setup(x => x.GetById(id)).Returns(coleta);

            var resultado = _service.ObterColetaPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.ColetaId);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        [InlineData(long.MaxValue)]
        public void DeletarColeta_ComDiferentesIds_DeveChamarRepositoryDelete(long id)
        {
            _service.DeletarColeta(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }
    }
}