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
    public class AlertaServiceTests
    {
        private readonly Mock<IAlertaRepository> _mockRepository;
        private readonly AlertaService _service;

        public AlertaServiceTests()
        {
            _mockRepository = new Mock<IAlertaRepository>();
            _service = new AlertaService(_mockRepository.Object);
        }

        [Fact]
        public void ObterTodosAlertas_DeveRetornarTodosAlertas()
        {
            var alertasEsperados = new List<AlertaModel>
            {
                new AlertaModel { AlertaId = 1, Mensagem = "Alerta 1", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = StatusAlerta.Ativo, DataAlerta = DateTime.Now },
                new AlertaModel { AlertaId = 2, Mensagem = "Alerta 2", TipoAlerta = TipoAlerta.LixeiraCheia, Status = StatusAlerta.EmAndamento, DataAlerta = DateTime.Now }
            };
            _mockRepository.Setup(x => x.GetAll()).Returns(alertasEsperados);

            var resultado = _service.ObterTodosAlertas();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterTodosAlertas_QuandoNaoHaAlertas_DeveRetornarListaVazia()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<AlertaModel>());

            var resultado = _service.ObterTodosAlertas();

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterAlertaPorId_ComIdValido_DeveRetornarAlerta()
        {
            long id = 1;
            var alertaEsperado = new AlertaModel { AlertaId = id, Mensagem = "Alerta 1", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = StatusAlerta.Ativo, DataAlerta = DateTime.Now };
            _mockRepository.Setup(x => x.GetById(id)).Returns(alertaEsperado);

            var resultado = _service.ObterAlertaPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.AlertaId);
            Assert.Equal("Alerta 1", resultado.Mensagem);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterAlertaPorId_ComIdInexistente_DeveRetornarNull()
        {
            long id = 999;
            _mockRepository.Setup(x => x.GetById(id)).Returns((AlertaModel)null);

            var resultado = _service.ObterAlertaPorId(id);

            Assert.Null(resultado);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterAlertasPorStatus_ComStatusValido_DeveRetornarAlertas()
        {
            string status = StatusAlerta.Ativo;
            var alertasEsperados = new List<AlertaModel>
            {
                new AlertaModel { AlertaId = 1, Mensagem = "Alerta 1", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = status, DataAlerta = DateTime.Now }
            };
            _mockRepository.Setup(x => x.GetByStatus(status)).Returns(alertasEsperados);

            var resultado = _service.ObterAlertasPorStatus(status);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(status, resultado.First().Status);
            _mockRepository.Verify(x => x.GetByStatus(status), Times.Once);
        }

        [Fact]
        public void ObterAlertasPorStatus_ComStatusInvalido_DeveRetornarListaVazia()
        {
            string status = "Invalido";
            _mockRepository.Setup(x => x.GetByStatus(status)).Returns(new List<AlertaModel>());

            var resultado = _service.ObterAlertasPorStatus(status);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            _mockRepository.Verify(x => x.GetByStatus(status), Times.Once);
        }

        [Fact]
        public void ObterAlertasPorTipo_ComTipoValido_DeveRetornarAlertas()
        {
            string tipo = TipoAlerta.LixeiraCheia;
            var alertasEsperados = new List<AlertaModel>
            {
                new AlertaModel { AlertaId = 1, Mensagem = "Alerta 1", TipoAlerta = tipo, Status = StatusAlerta.Ativo, DataAlerta = DateTime.Now }
            };
            _mockRepository.Setup(x => x.GetByTipo(tipo)).Returns(alertasEsperados);

            var resultado = _service.ObterAlertasPorTipo(tipo);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(tipo, resultado.First().TipoAlerta);
            _mockRepository.Verify(x => x.GetByTipo(tipo), Times.Once);
        }

        [Fact]
        public void ObterAlertasPorUsuario_ComUsuarioIdValido_DeveRetornarAlertas()
        {
            long usuarioId = 1;
            var alertasEsperados = new List<AlertaModel>
            {
                new AlertaModel { AlertaId = 1, Mensagem = "Alerta 1", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = StatusAlerta.Ativo, DataAlerta = DateTime.Now, UsuarioId = usuarioId }
            };
            _mockRepository.Setup(x => x.GetByUsuario(usuarioId)).Returns(alertasEsperados);

            var resultado = _service.ObterAlertasPorUsuario(usuarioId);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(usuarioId, resultado.First().UsuarioId);
            _mockRepository.Verify(x => x.GetByUsuario(usuarioId), Times.Once);
        }

        [Fact]
        public void ObterAlertasPorPeriodo_ComPeriodoValido_DeveRetornarAlertas()
        {
            var dataInicio = new DateTime(2025, 1, 1);
            var dataFim = new DateTime(2025, 1, 31);
            var alertasEsperados = new List<AlertaModel>
            {
                new AlertaModel { AlertaId = 1, Mensagem = "Alerta 1", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = StatusAlerta.Ativo, DataAlerta = new DateTime(2025, 1, 15) }
            };
            _mockRepository.Setup(x => x.GetByPeriodo(dataInicio, dataFim)).Returns(alertasEsperados);

            var resultado = _service.ObterAlertasPorPeriodo(dataInicio, dataFim);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(new DateTime(2025, 1, 15), resultado.First().DataAlerta);
            _mockRepository.Verify(x => x.GetByPeriodo(dataInicio, dataFim), Times.Once);
        }

        [Fact]
        public void AdicionarAlerta_ComAlertaValido_DeveChamarRepositoryAdd()
        {
            var alerta = new AlertaModel { AlertaId = 1, Mensagem = "Alerta 1", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = StatusAlerta.Ativo };

            _service.AdicionarAlerta(alerta);

            _mockRepository.Verify(x => x.Add(alerta), Times.Once);
            Assert.True(alerta.DataAlerta != default(DateTime));
        }
        

        [Fact]
        public void AtualizarAlerta_ComAlertaValido_DeveChamarRepositoryUpdate()
        {
            var alerta = new AlertaModel { AlertaId = 1, Mensagem = "Alerta Atualizado", TipoAlerta = TipoAlerta.LixeiraCheia, Status = StatusAlerta.Resolvido, DataAlerta = DateTime.Now };

            _service.AtualizarAlerta(alerta);

            _mockRepository.Verify(x => x.Update(alerta), Times.Once);
        }

        [Fact]
        public void AtualizarAlerta_ComAlertaNull_NaoDeveChamarRepositoryUpdate()
        {
            AlertaModel alerta = null;

            _service.AtualizarAlerta(alerta);

            _mockRepository.Verify(x => x.Update(It.IsAny<AlertaModel>()), Times.Once);
        }

        [Fact]
        public void DeletarAlerta_ComIdValido_DeveChamarRepositoryDelete()
        {
            long id = 1;

            _service.DeletarAlerta(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Fact]
        public void AtualizarStatusAlerta_ComIdValidoENovoStatus_DeveAtualizarStatus()
        {
            long id = 1;
            string novoStatus = StatusAlerta.Resolvido;
            var alerta = new AlertaModel { AlertaId = id, Mensagem = "Alerta 1", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = StatusAlerta.Ativo, DataAlerta = DateTime.Now };
            _mockRepository.Setup(x => x.GetById(id)).Returns(alerta);

            _service.AtualizarStatusAlerta(id, novoStatus);

            Assert.Equal(novoStatus, alerta.Status);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
            _mockRepository.Verify(x => x.Update(alerta), Times.Once);
        }

        [Fact]
        public void AtualizarStatusAlerta_ComIdInexistente_NaoDeveChamarUpdate()
        {
            long id = 999;
            string novoStatus = StatusAlerta.Resolvido;
            _mockRepository.Setup(x => x.GetById(id)).Returns((AlertaModel)null);

            _service.AtualizarStatusAlerta(id, novoStatus);

            _mockRepository.Verify(x => x.GetById(id), Times.Once);
            _mockRepository.Verify(x => x.Update(It.IsAny<AlertaModel>()), Times.Never);
        }

        [Fact]
        public void ContarAlertasPorStatus_ComStatusValido_DeveRetornarContagem()
        {
            string status = StatusAlerta.Ativo;
            long contagemEsperada = 5;
            _mockRepository.Setup(x => x.CountByStatus(status)).Returns(contagemEsperada);

            var resultado = _service.ContarAlertasPorStatus(status);

            Assert.Equal(contagemEsperada, resultado);
            _mockRepository.Verify(x => x.CountByStatus(status), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(999)]
        public void ObterAlertaPorId_ComDiferentesIds_DeveChamarRepositoryGetById(long id)
        {
            var alerta = new AlertaModel { AlertaId = id, Mensagem = $"Alerta {id}", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = StatusAlerta.Ativo, DataAlerta = DateTime.Now };
            _mockRepository.Setup(x => x.GetById(id)).Returns(alerta);

            var resultado = _service.ObterAlertaPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.AlertaId);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        [InlineData(long.MaxValue)]
        public void DeletarAlerta_ComDiferentesIds_DeveChamarRepositoryDelete(long id)
        {
            _service.DeletarAlerta(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Theory]
        [InlineData(TipoAlerta.ColetaAtrasada)]
        [InlineData(TipoAlerta.LixeiraCheia)]
        [InlineData(TipoAlerta.Outros)]
        public void ObterAlertasPorTipo_ComDiferentesTipos_DeveChamarRepositoryGetByTipo(string tipo)
        {
            var alertasEsperados = new List<AlertaModel>
            {
                new AlertaModel { AlertaId = 1, Mensagem = "Alerta 1", TipoAlerta = tipo, Status = StatusAlerta.Ativo, DataAlerta = DateTime.Now }
            };
            _mockRepository.Setup(x => x.GetByTipo(tipo)).Returns(alertasEsperados);

            var resultado = _service.ObterAlertasPorTipo(tipo);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(tipo, resultado.First().TipoAlerta);
            _mockRepository.Verify(x => x.GetByTipo(tipo), Times.Once);
        }

        [Theory]
        [InlineData(StatusAlerta.Ativo)]
        [InlineData(StatusAlerta.Resolvido)]
        [InlineData(StatusAlerta.EmAndamento)]
        public void ObterAlertasPorStatus_ComDiferentesStatus_DeveChamarRepositoryGetByStatus(string status)
        {
            var alertasEsperados = new List<AlertaModel>
            {
                new AlertaModel { AlertaId = 1, Mensagem = "Alerta 1", TipoAlerta = TipoAlerta.ColetaAtrasada, Status = status, DataAlerta = DateTime.Now }
            };
            _mockRepository.Setup(x => x.GetByStatus(status)).Returns(alertasEsperados);

            var resultado = _service.ObterAlertasPorStatus(status);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(status, resultado.First().Status);
            _mockRepository.Verify(x => x.GetByStatus(status), Times.Once);
        }
    }
}