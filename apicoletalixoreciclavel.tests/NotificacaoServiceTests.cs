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
    public class NotificacaoServiceTests
    {
        private readonly Mock<INotificacaoRepository> _mockRepository;
        private readonly NotificacaoService _service;

        public NotificacaoServiceTests()
        {
            _mockRepository = new Mock<INotificacaoRepository>();
            _service = new NotificacaoService(_mockRepository.Object);
        }

        [Fact]
        public void ObterNotificacaoPorId_DeveRetornarComDetalhes()
        {
            long id = 1;
            var notificacao = new NotificacaoModel { NotificacaoId = id, Titulo = "Titulo", Mensagem = "Msg" };
            _mockRepository.Setup(x => x.GetByIdWithDetails(id)).Returns(notificacao);

            var resultado = _service.ObterNotificacaoPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.NotificacaoId);
            _mockRepository.Verify(x => x.GetByIdWithDetails(id), Times.Once);
        }

        [Fact]
        public void ObterNotificacoesPorUsuario_DeveRetornarNotificacoes()
        {
            long usuarioId = 42;
            var notificacoes = new List<NotificacaoModel>
            {
                new NotificacaoModel { UsuarioId = usuarioId, Titulo = "X" }
            };

            _mockRepository.Setup(x => x.GetByUsuario(usuarioId)).Returns(notificacoes);

            var resultado = _service.ObterNotificacoesPorUsuario(usuarioId);

            Assert.Single(resultado);
            _mockRepository.Verify(x => x.GetByUsuario(usuarioId), Times.Once);
        }

        [Fact]
        public void ObterNotificacoesPorStatus_DeveRetornarListaFiltrada()
        {
            var status = "NaoLida";
            var notificacoes = new List<NotificacaoModel>
            {
                new NotificacaoModel { Status = status }
            };

            _mockRepository.Setup(x => x.GetByStatus(status)).Returns(notificacoes);

            var resultado = _service.ObterNotificacoesPorStatus(status);

            Assert.All(resultado, n => Assert.Equal(status, n.Status));
            _mockRepository.Verify(x => x.GetByStatus(status), Times.Once);
        }

        [Fact]
        public void ObterNotificacoesPorTipo_DeveRetornarListaFiltrada()
        {
            var tipo = "Sistema";
            var notificacoes = new List<NotificacaoModel>
            {
                new NotificacaoModel { Tipo = tipo }
            };

            _mockRepository.Setup(x => x.GetByTipo(tipo)).Returns(notificacoes);

            var resultado = _service.ObterNotificacoesPorTipo(tipo);

            Assert.All(resultado, n => Assert.Equal(tipo, n.Tipo));
            _mockRepository.Verify(x => x.GetByTipo(tipo), Times.Once);
        }

        [Fact]
        public void ObterNotificacaoNaoLidasPorUsuario_DeveRetornarNaoLidas()
        {
            long usuarioId = 5;
            var notificacoes = new List<NotificacaoModel>
            {
                new NotificacaoModel { UsuarioId = usuarioId, Status = "NaoLida" }
            };

            _mockRepository.Setup(x => x.GetNaoLidasByUsuario(usuarioId)).Returns(notificacoes);

            var resultado = _service.ObterNotificacaoNaoLidasPorUsuario(usuarioId);

            Assert.All(resultado, n => Assert.Equal("NaoLida", n.Status));
            _mockRepository.Verify(x => x.GetNaoLidasByUsuario(usuarioId), Times.Once);
        }

        [Fact]
        public void AdicionarNotificacao_Valida_DeveChamarAdd()
        {
            var notificacao = new NotificacaoModel
            {
                Titulo = "Nova Notificação",
                Mensagem = "Conteúdo",
                Tipo = "Sistema"
            };

            _service.AdicionarNotificacao(notificacao);

            _mockRepository.Verify(x => x.Add(It.Is<NotificacaoModel>(
                n => n.Titulo == "Nova Notificação" && n.Status == "NaoLida")), Times.Once);
        }

        [Fact]
        public void AdicionarNotificacao_SemTitulo_DeveLancarExcecao()
        {
            var notificacao = new NotificacaoModel { Mensagem = "Mensagem", Tipo = "Sistema" };

            var ex = Assert.Throws<ArgumentException>(() => _service.AdicionarNotificacao(notificacao));
            Assert.Equal("O título da notificação é obrigatório.", ex.Message);
        }

        [Fact]
        public void AdicionarNotificacao_SemMensagem_DeveLancarExcecao()
        {
            var notificacao = new NotificacaoModel { Titulo = "Título", Tipo = "Sistema" };

            var ex = Assert.Throws<ArgumentException>(() => _service.AdicionarNotificacao(notificacao));
            Assert.Equal("A mensagem da notificação é obrigatória.", ex.Message);
        }

        [Fact]
        public void AtualizarNotificacao_Valida_DeveChamarUpdate()
        {
            var notificacao = new NotificacaoModel
            {
                NotificacaoId = 1,
                Titulo = "Atualizada",
                Mensagem = "Mensagem atualizada",
                Tipo = "Sistema"
            };

            _service.AtualizarNotificacao(notificacao);

            _mockRepository.Verify(x => x.Update(notificacao), Times.Once);
        }

        [Fact]
        public void AtualizarNotificacao_SemTitulo_DeveLancarExcecao()
        {
            var notificacao = new NotificacaoModel { Mensagem = "Mensagem" };

            var ex = Assert.Throws<ArgumentException>(() => _service.AtualizarNotificacao(notificacao));
            Assert.Equal("O título da notificação é obrigatório.", ex.Message);
        }

        [Fact]
        public void AtualizarNotificacao_SemMensagem_DeveLancarExcecao()
        {
            var notificacao = new NotificacaoModel { Titulo = "Título" };

            var ex = Assert.Throws<ArgumentException>(() => _service.AtualizarNotificacao(notificacao));
            Assert.Equal("A mensagem da notificação é obrigatória.", ex.Message);
        }

        [Fact]
        public void DeletarNotificacao_DeveChamarDelete()
        {
            long id = 5;

            _service.DeletarNotificacao(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Fact]
        public void MarcarComoLida_DeveChamarRepository()
        {
            long id = 3;

            _service.MarcarComoLida(id);

            _mockRepository.Verify(x => x.MarcarComoLida(id), Times.Once);
        }

        [Fact]
        public void MarcarComoArquivada_DeveChamarRepository()
        {
            long id = 4;

            _service.MarcarComoArquivada(id);

            _mockRepository.Verify(x => x.MarcarComoArquivada(id), Times.Once);
        }

        [Fact]
        public void ContarNaoLidasPorUsuario_DeveRetornarNumero()
        {
            long usuarioId = 10;
            _mockRepository.Setup(x => x.CountNaoLidasByUsuario(usuarioId)).Returns(7);

            var resultado = _service.ContarNaoLidasPorUsuario(usuarioId);

            Assert.Equal(7, resultado);
            _mockRepository.Verify(x => x.CountNaoLidasByUsuario(usuarioId), Times.Once);
        }
    }
}
