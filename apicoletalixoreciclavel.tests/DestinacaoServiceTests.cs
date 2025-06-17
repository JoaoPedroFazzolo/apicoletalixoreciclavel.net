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
    public class DestinacaoServiceTests
    {
        private readonly Mock<IDestinacaoRepository> _mockRepository;
        private readonly DestinacaoService _service;

        public DestinacaoServiceTests()
        {
            _mockRepository = new Mock<IDestinacaoRepository>();
            _service = new DestinacaoService(_mockRepository.Object);
        }

        [Fact]
        public void ObterTodasDestinacoes_DeveRetornarTodasDestinacoes()
        {
            var destinacoesEsperadas = new List<DestinacaoModel>
            {
                new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1", CapacidadeMaxima = 100 },
                new DestinacaoModel { DestinacaoId = 2, Nome = "Destinacao B", Tipo = "Compostagem", Endereco = "Rua 2", CapacidadeMaxima = 200 }
            };
            _mockRepository.Setup(x => x.GetAll()).Returns(destinacoesEsperadas);

            var resultado = _service.ObterTodasDestinacoes();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterTodasDestinacoes_QuandoNaoHaDestinacoes_DeveRetornarListaVazia()
        {
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<DestinacaoModel>());

            var resultado = _service.ObterTodasDestinacoes();

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            _mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterDestinacaoPorId_ComIdValido_DeveRetornarDestinacao()
        {
            long id = 1;
            var destinacaoEsperada = new DestinacaoModel { DestinacaoId = id, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1", CapacidadeMaxima = 100 };
            _mockRepository.Setup(x => x.GetById(id)).Returns(destinacaoEsperada);

            var resultado = _service.ObterDestinacaoPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.DestinacaoId);
            Assert.Equal("Destinacao A", resultado.Nome);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterDestinacaoPorId_ComIdInexistente_DeveRetornarNull()
        {
            long id = 999;
            _mockRepository.Setup(x => x.GetById(id)).Returns((DestinacaoModel)null);

            var resultado = _service.ObterDestinacaoPorId(id);

            Assert.Null(resultado);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void ObterDestinacoesPorTipo_ComTipoValido_DeveRetornarDestinacoes()
        {
            string tipo = "Reciclagem";
            var destinacoesEsperadas = new List<DestinacaoModel>
            {
                new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = tipo, Endereco = "Rua 1", CapacidadeMaxima = 100 }
            };
            _mockRepository.Setup(x => x.GetByTipo(tipo)).Returns(destinacoesEsperadas);

            var resultado = _service.ObterDestinacoesPorTipo(tipo);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(tipo, resultado.First().Tipo);
            _mockRepository.Verify(x => x.GetByTipo(tipo), Times.Once);
        }

        [Fact]
        public void ObterDestinacoesPorTipo_ComTipoNulo_DeveLancarArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => _service.ObterDestinacoesPorTipo(null));
            Assert.Equal("O tipo da destinação é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.GetByTipo(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ObterDestinacoesPorStatus_ComStatusValido_DeveRetornarDestinacoes()
        {
            string status = "Ativo";
            var destinacoesEsperadas = new List<DestinacaoModel>
            {
                new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1", Status = status }
            };
            _mockRepository.Setup(x => x.GetByStatus(status)).Returns(destinacoesEsperadas);

            var resultado = _service.ObterDestinacoesPorStatus(status);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(status, resultado.First().Status);
            _mockRepository.Verify(x => x.GetByStatus(status), Times.Once);
        }

        [Fact]
        public void ObterDestinacoesPorStatus_ComStatusNulo_DeveLancarArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => _service.ObterDestinacoesPorStatus(null));
            Assert.Equal("O status da destinação é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.GetByStatus(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ObterDestinacoesAtivas_DeveRetornarDestinacoesAtivas()
        {
            var destinacoesEsperadas = new List<DestinacaoModel>
            {
                new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1", Status = "Ativo" }
            };
            _mockRepository.Setup(x => x.GetAtivas()).Returns(destinacoesEsperadas);

            var resultado = _service.ObterDestinacoesAtivas();

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal("Ativo", resultado.First().Status);
            _mockRepository.Verify(x => x.GetAtivas(), Times.Once);
        }

        [Fact]
        public void ObterDestinacoesQuePermitemColeta_DeveRetornarDestinacoesQuePermitemColeta()
        {
            var destinacoesEsperadas = new List<DestinacaoModel>
            {
                new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1", PermiteColeta = true }
            };
            _mockRepository.Setup(x => x.GetQuePermitemColeta()).Returns(destinacoesEsperadas);

            var resultado = _service.ObterDestinacoesQuePermitemColeta();

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.True(resultado.First().PermiteColeta);
            _mockRepository.Verify(x => x.GetQuePermitemColeta(), Times.Once);
        }

        [Fact]
        public void PesquisarPorNome_ComNomeValido_DeveRetornarDestinacoes()
        {
            string nome = "Destinacao A";
            var destinacoesEsperadas = new List<DestinacaoModel>
            {
                new DestinacaoModel { DestinacaoId = 1, Nome = nome, Tipo = "Reciclagem", Endereco = "Rua 1" }
            };
            _mockRepository.Setup(x => x.SearchByName(nome)).Returns(destinacoesEsperadas);

            var resultado = _service.PesquisarPorNome(nome);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(nome, resultado.First().Nome);
            _mockRepository.Verify(x => x.SearchByName(nome), Times.Once);
        }

        [Fact]
        public void PesquisarPorNome_ComNomeNulo_DeveLancarArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => _service.PesquisarPorNome(null));
            Assert.Equal("O nome para pesquisa é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.SearchByName(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void AdicionarDestinacao_ComDestinacaoValida_DeveChamarRepositoryAdd()
        {
            var destinacao = new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            _mockRepository.Setup(x => x.ExistsByName(destinacao.Nome)).Returns(false);

            _service.AdicionarDestinacao(destinacao);

            Assert.Equal("Ativo", destinacao.Status);
            Assert.True(destinacao.DataCadastro <= DateTime.Now);
            _mockRepository.Verify(x => x.Add(destinacao), Times.Once);
            _mockRepository.Verify(x => x.ExistsByName(destinacao.Nome), Times.Once);
        }

        [Fact]
        public void AdicionarDestinacao_ComNomeNulo_DeveLancarArgumentException()
        {
            var destinacao = new DestinacaoModel { Nome = null, Tipo = "Reciclagem", Endereco = "Rua 1" };

            var exception = Assert.Throws<ArgumentException>(() => _service.AdicionarDestinacao(destinacao));
            Assert.Equal("O nome da destinação é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.Add(It.IsAny<DestinacaoModel>()), Times.Never);
        }

        [Fact]
        public void AdicionarDestinacao_ComTipoNulo_DeveLancarArgumentException()
        {
            var destinacao = new DestinacaoModel { Nome = "Destinacao A", Tipo = null, Endereco = "Rua 1" };

            var exception = Assert.Throws<ArgumentException>(() => _service.AdicionarDestinacao(destinacao));
            Assert.Equal("O tipo da destinação é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.Add(It.IsAny<DestinacaoModel>()), Times.Never);
        }

        [Fact]
        public void AdicionarDestinacao_ComEnderecoNulo_DeveLancarArgumentException()
        {
            var destinacao = new DestinacaoModel { Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = null };

            var exception = Assert.Throws<ArgumentException>(() => _service.AdicionarDestinacao(destinacao));
            Assert.Equal("O endereço da destinação é obrigatório.", exception.Message);
            _mockRepository.Verify(x => x.Add(It.IsAny<DestinacaoModel>()), Times.Never);
        }

        [Fact]
        public void AdicionarDestinacao_ComNomeExistente_DeveLancarInvalidOperationException()
        {
            var destinacao = new DestinacaoModel { Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            _mockRepository.Setup(x => x.ExistsByName(destinacao.Nome)).Returns(true);

            var exception = Assert.Throws<InvalidOperationException>(() => _service.AdicionarDestinacao(destinacao));
            Assert.Equal("Já existe uma destinação com este nome.", exception.Message);
            _mockRepository.Verify(x => x.Add(It.IsAny<DestinacaoModel>()), Times.Never);
        }

        [Fact]
        public void AtualizarDestinacao_ComDestinacaoValida_DeveChamarRepositoryUpdate()
        {
            var destinacao = new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            _mockRepository.Setup(x => x.GetById(destinacao.DestinacaoId)).Returns(destinacao);
            _mockRepository.Setup(x => x.SearchByName(destinacao.Nome)).Returns(new List<DestinacaoModel>());

            _service.AtualizarDestinacao(destinacao);

            _mockRepository.Verify(x => x.Update(destinacao), Times.Once);
            _mockRepository.Verify(x => x.GetById(destinacao.DestinacaoId), Times.Once);
            _mockRepository.Verify(x => x.SearchByName(destinacao.Nome), Times.Once);
        }

        [Fact]
        public void AtualizarDestinacao_ComDestinacaoNaoExistente_DeveLancarArgumentException()
        {
            var destinacao = new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            _mockRepository.Setup(x => x.GetById(destinacao.DestinacaoId)).Returns((DestinacaoModel)null);

            var exception = Assert.Throws<ArgumentException>(() => _service.AtualizarDestinacao(destinacao));
            Assert.Equal("Destinação não encontrada.", exception.Message);
            _mockRepository.Verify(x => x.Update(It.IsAny<DestinacaoModel>()), Times.Never);
        }

        [Fact]
        public void AtualizarDestinacao_ComNomeExistente_DeveLancarInvalidOperationException()
        {
            var destinacao = new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            var outraDestinacao = new DestinacaoModel { DestinacaoId = 2, Nome = "Destinacao A", Tipo = "Compostagem", Endereco = "Rua 2" };
            _mockRepository.Setup(x => x.GetById(destinacao.DestinacaoId)).Returns(destinacao);
            _mockRepository.Setup(x => x.SearchByName(destinacao.Nome)).Returns(new List<DestinacaoModel> { outraDestinacao });

            var exception = Assert.Throws<InvalidOperationException>(() => _service.AtualizarDestinacao(destinacao));
            Assert.Equal("Já existe uma destinação com este nome.", exception.Message);
            _mockRepository.Verify(x => x.Update(It.IsAny<DestinacaoModel>()), Times.Never);
        }

        [Fact]
        public void DeletarDestinacao_ComIdValido_DeveChamarRepositoryDelete()
        {
            long id = 1;
            var destinacao = new DestinacaoModel { DestinacaoId = id, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            _mockRepository.Setup(x => x.GetById(id)).Returns(destinacao);

            _service.DeletarDestinacao(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void DeletarDestinacao_ComIdInexistente_DeveLancarArgumentException()
        {
            long id = 999;
            _mockRepository.Setup(x => x.GetById(id)).Returns((DestinacaoModel)null);

            var exception = Assert.Throws<ArgumentException>(() => _service.DeletarDestinacao(id));
            Assert.Equal("Destinação não encontrada.", exception.Message);
            _mockRepository.Verify(x => x.Delete(id), Times.Never);
        }

        [Fact]
        public void AlterarStatus_ComStatusValido_DeveChamarRepositoryAlterarStatus()
        {
            long id = 1;
            string status = "Inativo";
            var destinacao = new DestinacaoModel { DestinacaoId = id, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            _mockRepository.Setup(x => x.GetById(id)).Returns(destinacao);

            _service.AlterarStatus(id, status);

            _mockRepository.Verify(x => x.AlterarStatus(id, status), Times.Once);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void AlterarStatus_ComStatusInvalido_DeveLancarArgumentException()
        {
            long id = 1;
            string status = "Inválido";
            var destinacao = new DestinacaoModel { DestinacaoId = id, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            _mockRepository.Setup(x => x.GetById(id)).Returns(destinacao);

            var exception = Assert.Throws<ArgumentException>(() => _service.AlterarStatus(id, status));
            Assert.Equal("Status inválido. Valores aceitos: Ativo, Inativo, Suspenso.", exception.Message);
            _mockRepository.Verify(x => x.AlterarStatus(id, It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void AlterarStatus_ComDestinacaoInexistente_DeveLancarArgumentException()
        {
            long id = 999;
            string status = "Inativo";
            _mockRepository.Setup(x => x.GetById(id)).Returns((DestinacaoModel)null);

            var exception = Assert.Throws<ArgumentException>(() => _service.AlterarStatus(id, status));
            Assert.Equal("Destinação não encontrada.", exception.Message);
            _mockRepository.Verify(x => x.AlterarStatus(id, It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ObterTodosTipos_DeveRetornarListaDeTipos()
        {
            var tiposEsperados = new List<string> { "Reciclagem", "Compostagem", "Aterro" };
            _mockRepository.Setup(x => x.GetAllTipos()).Returns(tiposEsperados);

            var resultado = _service.ObterTodosTipos();

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());
            Assert.Contains("Reciclagem", resultado);
            _mockRepository.Verify(x => x.GetAllTipos(), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(999)]
        public void ObterDestinacaoPorId_ComDiferentesIds_DeveChamarRepositoryGetById(long id)
        {
            var destinacao = new DestinacaoModel { DestinacaoId = id, Nome = $"Destinacao {id}", Tipo = "Reciclagem", Endereco = $"Rua {id}" };
            _mockRepository.Setup(x => x.GetById(id)).Returns(destinacao);

            var resultado = _service.ObterDestinacaoPorId(id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.DestinacaoId);
            _mockRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Theory]
        [InlineData("Reciclagem")]
        [InlineData("Compostagem")]
        [InlineData("Aterro")]
        public void ObterDestinacoesPorTipo_ComDiferentesTipos_DeveChamarRepositoryGetByTipo(string tipo)
        {
            var destinacoesEsperadas = new List<DestinacaoModel>
            {
                new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = tipo, Endereco = "Rua 1" }
            };
            _mockRepository.Setup(x => x.GetByTipo(tipo)).Returns(destinacoesEsperadas);

            var resultado = _service.ObterDestinacoesPorTipo(tipo);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(tipo, resultado.First().Tipo);
            _mockRepository.Verify(x => x.GetByTipo(tipo), Times.Once);
        }

        [Theory]
        [InlineData("Ativo")]
        [InlineData("Inativo")]
        [InlineData("Suspenso")]
        public void ObterDestinacoesPorStatus_ComDiferentesStatus_DeveChamarRepositoryGetByStatus(string status)
        {
            var destinacoesEsperadas = new List<DestinacaoModel>
            {
                new DestinacaoModel { DestinacaoId = 1, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1", Status = status }
            };
            _mockRepository.Setup(x => x.GetByStatus(status)).Returns(destinacoesEsperadas);

            var resultado = _service.ObterDestinacoesPorStatus(status);

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(status, resultado.First().Status);
            _mockRepository.Verify(x => x.GetByStatus(status), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(long.MaxValue)]
        public void DeletarDestinacao_ComDiferentesIds_DeveChamarRepositoryDelete(long id)
        {
            var destinacao = new DestinacaoModel { DestinacaoId = id, Nome = "Destinacao A", Tipo = "Reciclagem", Endereco = "Rua 1" };
            _mockRepository.Setup(x => x.GetById(id)).Returns(destinacao);

            _service.DeletarDestinacao(id);

            _mockRepository.Verify(x => x.Delete(id), Times.Once);
        }
    }
}