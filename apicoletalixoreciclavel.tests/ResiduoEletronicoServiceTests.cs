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
    public class ResiduoEletronicoServiceTests
    {
        private readonly Mock<IResiduoEletronicoRepository> _mockRepository;
        private readonly ResiduoEletronicoService _service;

        public ResiduoEletronicoServiceTests()
        {
            _mockRepository = new Mock<IResiduoEletronicoRepository>();
            _service = new ResiduoEletronicoService(_mockRepository.Object);
        }

        [Fact]
        public void ObterTodosResiduoEletronicos_DeveRetornarTodosResiduos()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            
            var residuosEsperados = new List<ResiduoEletronicoModel>
            {
                new ResiduoEletronicoModel { ResiduoEletronicoId = 1, Tipo = "Smartphone", Marca = "Samsung", Modelo = "Galaxy S21" },
                new ResiduoEletronicoModel { ResiduoEletronicoId = 2, Tipo = "Notebook", Marca = "Dell", Modelo = "Inspiron 15" }
            };

            _mockRepository.Setup(x => x.GetAll(pageNumber, pageSize)).Returns(residuosEsperados);

            // Act
            var resultado = _service.ObterTodosResiduoEletronicos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAll(pageNumber, pageSize), Times.Once);
        }

        [Fact]
        public void ObterTodosResiduoEletronicos_QuandoNaoHaResiduos_DeveRetornarListaVazia()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            
            _mockRepository.Setup(x => x.GetAll(pageNumber, pageSize)).Returns(new List<ResiduoEletronicoModel>());

            // Act
            var resultado = _service.ObterTodosResiduoEletronicos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            _mockRepository.Verify(x => x.GetAll(pageNumber, pageSize), Times.Once);
        }
        

        [Fact]
        public void ObterTodosResiduoEletronicosComDetalhes_DeveRetornarTodosResiduosComDetalhes()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            
            var residuosEsperados = new List<ResiduoEletronicoModel>
            {
                new ResiduoEletronicoModel { ResiduoEletronicoId = 1, Tipo = "Smartphone", Marca = "Samsung" },
                new ResiduoEletronicoModel { ResiduoEletronicoId = 2, Tipo = "Notebook", Marca = "Dell" }
            };

            _mockRepository.Setup(x => x.GetAllWithDetails(pageNumber, pageSize)).Returns(residuosEsperados);

            // Act
            var resultado = _service.ObterTodosResiduoEletronicosComDetalhes();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAllWithDetails(pageNumber, pageSize), Times.Once);
        }
        

        [Fact]
        public void ObterResiduoEletronicoPorId_ComIdValido_DeveRetornarResiduo()
        {
            // Arrange
            long id = 1;
            var residuoEsperado = new ResiduoEletronicoModel { ResiduoEletronicoId = id, Tipo = "Smartphone", Marca = "Samsung", Modelo = "Galaxy S21" };
            _mockRepository.Setup(x => x.GetById(id)).Returns(residuoEsperado);

            // Act
            var resultado = _service.ObterResiduoEletronicoPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.ResiduoEletronicoId);
            Assert.Equal("Smartphone", resultado.Tipo);
            Assert.Equal("Samsung", resultado.Marca);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterResiduoEletronicoPorId_ComIdInexistente_DeveRetornarNull()
        {
            // Arrange
            long id = 999;
            _mockRepository.Setup(x => x.GetById(id)).Returns((ResiduoEletronicoModel)null);

            // Act
            var resultado = _service.ObterResiduoEletronicoPorId(id);

            // Assert
            Assert.Null(resultado);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }
        

        [Fact]
        public void ObterResiduoEletronicoPorIdComDetalhes_ComIdValido_DeveRetornarResiduoComDetalhes()
        {
            // Arrange
            long id = 1;
            var residuoEsperado = new ResiduoEletronicoModel { ResiduoEletronicoId = id, Tipo = "Smartphone", Marca = "Apple" };
            _mockRepository.Setup(x => x.GetByIdWithDetails(id)).Returns(residuoEsperado);

            // Act
            var resultado = _service.ObterResiduoEletronicoPorIdComDetalhes(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.ResiduoEletronicoId);
            _mockRepository.Verify(x => x.GetByIdWithDetails(id), Times.Once);
        }
        

        [Fact]
        public void AdicionarResiduoEletronico_ComResiduoValido_DeveChamarRepositoryAdd()
        {
            // Arrange
            var residuo = new ResiduoEletronicoModel 
            { 
                ResiduoEletronicoId = 1, 
                Tipo = "Smartphone", 
                Marca = "Apple", 
                Modelo = "iPhone 13",
                Estado = "Usado",
                Status = "Disponível",
                UsuarioId = 1
            };

            // Act
            _service.AdicionarResiduoEletronico(residuo);

            // Assert
            _mockRepository.Verify(x => x.Add(residuo), Times.Once);
        }

        [Fact]
        public void AdicionarResiduoEletronico_ComResiduoNull_DeveChamarRepositoryAdd()
        {
            // Arrange
            ResiduoEletronicoModel residuo = null;

            // Act
            _service.AdicionarResiduoEletronico(residuo);

            // Assert
            _mockRepository.Verify(x => x.Add(residuo), Times.Once);
        }

        [Fact]
        public void AtualizarResiduoEletronico_ComResiduoValido_DeveChamarRepositoryUpdate()
        {
            // Arrange
            var residuo = new ResiduoEletronicoModel 
            { 
                ResiduoEletronicoId = 1, 
                Tipo = "Notebook", 
                Marca = "Dell", 
                Modelo = "Inspiron 15 Atualizado",
                Estado = "Novo",
                Status = "Reservado",
                UsuarioId = 2
            };

            // Act
            _service.AtualizarResiduoEletronico(residuo);

            // Assert
            _mockRepository.Verify(x => x.Update(residuo), Times.Once);
        }

        [Fact]
        public void AtualizarResiduoEletronico_ComResiduoNull_DeveLancarArgumentException()
        {
            // Arrange
            ResiduoEletronicoModel residuo = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => 
                _service.AtualizarResiduoEletronico(residuo));
            
            Assert.Equal("ResiduoEletronicoModel is null", exception.Message);
        }

        [Fact]
        public void AtualizarResiduoEletronico_ComResiduoNull_NaoDeveChamarRepositoryUpdate()
        {
            // Arrange
            ResiduoEletronicoModel residuo = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                _service.AtualizarResiduoEletronico(residuo));
            
            _mockRepository.Verify(x => x.Update(It.IsAny<ResiduoEletronicoModel>()), Times.Never);
        }
        

        [Fact]
        public void DeletarResiduoEletronico_ComIdValido_DeveChamarRepositoryDelete()
        {
            // Arrange
            long id = 1;

            // Act
            _service.DeletarResiduoEletronico(id);

            // Assert
            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Fact]
        public void DeletarResiduoEletronico_ComIdZero_DeveChamarRepositoryDelete()
        {
            // Arrange
            long id = 0;

            // Act
            _service.DeletarResiduoEletronico(id);

            // Assert
            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Fact]
        public void DeletarResiduoEletronico_ComIdNegativo_DeveChamarRepositoryDelete()
        {
            // Arrange
            long id = -1;

            // Act
            _service.DeletarResiduoEletronico(id);

            // Assert
            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(999)]
        public void ObterResiduoEletronicoPorId_ComDiferentesIds_DeveChamarRepositoryGetById(long id)
        {
            // Arrange
            var residuo = new ResiduoEletronicoModel 
            { 
                ResiduoEletronicoId = id, 
                Tipo = "Tablet", 
                Marca = "Samsung", 
                Modelo = $"Galaxy Tab {id}",
                Estado = "Usado",
                Status = "Disponível",
                UsuarioId = id
            };
            _mockRepository.Setup(x => x.GetById(id)).Returns(residuo);

            // Act
            var resultado = _service.ObterResiduoEletronicoPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.ResiduoEletronicoId);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        [InlineData(long.MaxValue)]
        public void DeletarResiduoEletronico_ComDiferentesIds_DeveChamarRepositoryDelete(long id)
        {
            // Act
            _service.DeletarResiduoEletronico(id);

            // Assert
            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }
    }
}