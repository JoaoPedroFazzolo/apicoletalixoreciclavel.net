using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apicoletalixoreciclavel.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class DestinacaoController : ControllerBase
    {
        private readonly IDestinacaoService _destinacaoService;
        private readonly IMapper _mapper;

        public DestinacaoController(IDestinacaoService destinacaoService, IMapper mapper)
        {
            _destinacaoService = destinacaoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todas as destinações
        /// </summary>
        /// <returns>Lista de destinações</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DestinacaoViewModel>), 200)]
        public ActionResult<IEnumerable<DestinacaoViewModel>> GetAll()
        {
            var destinacoes = _destinacaoService.ObterTodasDestinacoes();
            var destinacoesViewModel = _mapper.Map<IEnumerable<DestinacaoViewModel>>(destinacoes);
            return Ok(destinacoesViewModel);
        }

        /// <summary>
        /// Busca uma destinação por ID
        /// </summary>
        /// <param name="id">ID da destinação</param>
        /// <returns>Destinação encontrada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DestinacaoViewModel), 200)]
        [ProducesResponseType(404)]
        public ActionResult<DestinacaoViewModel> GetById([FromRoute] long id)
        {
            var destinacao = _destinacaoService.ObterDestinacaoPorId(id);
            if (destinacao == null)
                return NotFound();

            var destinacaoViewModel = _mapper.Map<DestinacaoViewModel>(destinacao);
            return Ok(destinacaoViewModel);
        }

        /// <summary>
        /// Lista destinações ativas
        /// </summary>
        /// <returns>Lista de destinações ativas</returns>
        [HttpGet("ativas")]
        [ProducesResponseType(typeof(IEnumerable<DestinacaoViewModel>), 200)]
        public ActionResult<IEnumerable<DestinacaoViewModel>> GetAtivas()
        {
            var destinacoes = _destinacaoService.ObterDestinacoesAtivas();
            var destinacoesViewModel = _mapper.Map<IEnumerable<DestinacaoViewModel>>(destinacoes);
            return Ok(destinacoesViewModel);
        }

        /// <summary>
        /// Lista destinações que permitem coleta
        /// </summary>
        /// <returns>Lista de destinações que permitem coleta</returns>
        [HttpGet("permite-coleta")]
        [ProducesResponseType(typeof(IEnumerable<DestinacaoViewModel>), 200)]
        public ActionResult<IEnumerable<DestinacaoViewModel>> GetQuePermitemColeta()
        {
            var destinacoes = _destinacaoService.ObterDestinacoesQuePermitemColeta();
            var destinacoesViewModel = _mapper.Map<IEnumerable<DestinacaoViewModel>>(destinacoes);
            return Ok(destinacoesViewModel);
        }

        /// <summary>
        /// Busca destinações por tipo
        /// </summary>
        /// <param name="tipo">Tipo da destinação</param>
        /// <returns>Lista de destinações do tipo especificado</returns>
        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(typeof(IEnumerable<DestinacaoViewModel>), 200)]
        public ActionResult<IEnumerable<DestinacaoViewModel>> GetByTipo([FromRoute] string tipo)
        {
            var destinacoes = _destinacaoService.ObterDestinacoesPorTipo(tipo);
            var destinacoesViewModel = _mapper.Map<IEnumerable<DestinacaoViewModel>>(destinacoes);
            return Ok(destinacoesViewModel);
        }

        /// <summary>
        /// Busca destinações por status
        /// </summary>
        /// <param name="status">Status da destinação</param>
        /// <returns>Lista de destinações com o status especificado</returns>
        [HttpGet("status/{status}")]
        [ProducesResponseType(typeof(IEnumerable<DestinacaoViewModel>), 200)]
        public ActionResult<IEnumerable<DestinacaoViewModel>> GetByStatus([FromRoute] string status)
        {
            var destinacoes = _destinacaoService.ObterDestinacoesPorStatus(status);
            var destinacoesViewModel = _mapper.Map<IEnumerable<DestinacaoViewModel>>(destinacoes);
            return Ok(destinacoesViewModel);
        }

        /// <summary>
        /// Pesquisa destinações por nome
        /// </summary>
        /// <param name="nome">Nome para pesquisa</param>
        /// <returns>Lista de destinações encontradas</returns>
        [HttpGet("pesquisar/{nome}")]
        [ProducesResponseType(typeof(IEnumerable<DestinacaoViewModel>), 200)]
        public ActionResult<IEnumerable<DestinacaoViewModel>> SearchByName([FromRoute] string nome)
        {
            var destinacoes = _destinacaoService.PesquisarPorNome(nome);
            var destinacoesViewModel = _mapper.Map<IEnumerable<DestinacaoViewModel>>(destinacoes);
            return Ok(destinacoesViewModel);
        }

        /// <summary>
        /// Lista todos os tipos de destinação
        /// </summary>
        /// <returns>Lista de tipos</returns>
        [HttpGet("tipos")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public ActionResult<IEnumerable<string>> GetTipos()
        {
            var tipos = _destinacaoService.ObterTodosTipos();
            return Ok(tipos);
        }

        /// <summary>
        /// Cria uma nova destinação
        /// </summary>
        /// <param name="viewModel">Dados da destinação</param>
        /// <returns>Destinação criada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(DestinacaoViewModel), 201)]
        [ProducesResponseType(400)]
        public ActionResult<DestinacaoViewModel> Post([FromBody] CreateDestinacaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var destinacao = _mapper.Map<DestinacaoModel>(viewModel);
                _destinacaoService.AdicionarDestinacao(destinacao);

                var destinacaoViewModel = _mapper.Map<DestinacaoViewModel>(destinacao);
                return CreatedAtAction(nameof(GetById), new { id = destinacao.DestinacaoId }, destinacaoViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma destinação existente
        /// </summary>
        /// <param name="id">ID da destinação</param>
        /// <param name="viewModel">Dados atualizados da destinação</param>
        /// <returns>Destinação atualizada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DestinacaoViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<DestinacaoViewModel> Put([FromRoute] long id, [FromBody] UpdateDestinacaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var destinacaoExistente = _destinacaoService.ObterDestinacaoPorId(id);
                if (destinacaoExistente == null)
                    return NotFound();

                _mapper.Map(viewModel, destinacaoExistente);
                _destinacaoService.AtualizarDestinacao(destinacaoExistente);

                var destinacaoViewModel = _mapper.Map<DestinacaoViewModel>(destinacaoExistente);
                return Ok(destinacaoViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera o status de uma destinação
        /// </summary>
        /// <param name="id">ID da destinação</param>
        /// <param name="status">Novo status</param>
        /// <returns>Confirmação da alteração</returns>
        [HttpPatch("{id}/status/{status}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult AlterarStatus([FromRoute] long id, [FromRoute] string status)
        {
            try
            {
                _destinacaoService.AlterarStatus(id, status);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma destinação
        /// </summary>
        /// <param name="id">ID da destinação</param>
        /// <returns>Confirmação da remoção</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete([FromRoute] long id)
        {
            try
            {
                _destinacaoService.DeletarDestinacao(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}