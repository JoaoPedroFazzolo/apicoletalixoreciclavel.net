using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apicoletalixoreciclavel.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _notificacaoService;
        private readonly IMapper _mapper;

        public NotificacaoController(INotificacaoService notificacaoService, IMapper mapper)
        {
            _notificacaoService = notificacaoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todas as notificações
        /// </summary>
        /// <returns>Lista de notificações</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NotificacaoViewModel>), 200)]
        public ActionResult<IEnumerable<NotificacaoViewModel>> GetAll()
        {
            var notificacoes = _notificacaoService.ObterTodasNotificacoes();
            var notificacoesViewModel = _mapper.Map<IEnumerable<NotificacaoViewModel>>(notificacoes);
            return Ok(notificacoesViewModel);
        }

        /// <summary>
        /// Busca uma notificação por ID
        /// </summary>
        /// <param name="id">ID da notificação</param>
        /// <returns>Notificação encontrada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NotificacaoViewModel), 200)]
        [ProducesResponseType(404)]
        public ActionResult<NotificacaoViewModel> GetById([FromRoute] long id)
        {
            var notificacao = _notificacaoService.ObterNotificacaoPorId(id);
            if (notificacao == null)
                return NotFound();

            var notificacaoViewModel = _mapper.Map<NotificacaoViewModel>(notificacao);
            return Ok(notificacaoViewModel);
        }

        /// <summary>
        /// Busca notificações por usuário
        /// </summary>
        /// <param name="usuarioId">ID do usuário</param>
        /// <returns>Lista de notificações do usuário</returns>
        [HttpGet("usuario/{usuarioId}")]
        [ProducesResponseType(typeof(IEnumerable<NotificacaoViewModel>), 200)]
        public ActionResult<IEnumerable<NotificacaoViewModel>> GetByUsuario([FromRoute] long usuarioId)
        {
            var notificacoes = _notificacaoService.ObterNotificacoesPorUsuario(usuarioId);
            var notificacoesViewModel = _mapper.Map<IEnumerable<NotificacaoViewModel>>(notificacoes);
            return Ok(notificacoesViewModel);
        }

        /// <summary>
        /// Busca notificações não lidas por usuário
        /// </summary>
        /// <param name="usuarioId">ID do usuário</param>
        /// <returns>Lista de notificações não lidas do usuário</returns>
        [HttpGet("usuario/{usuarioId}/nao-lidas")]
        [ProducesResponseType(typeof(IEnumerable<NotificacaoViewModel>), 200)]
        public ActionResult<IEnumerable<NotificacaoViewModel>> GetNaoLidasByUsuario([FromRoute] long usuarioId)
        {
            var notificacoes = _notificacaoService.ObterNotificacaoNaoLidasPorUsuario(usuarioId);
            var notificacoesViewModel = _mapper.Map<IEnumerable<NotificacaoViewModel>>(notificacoes);
            return Ok(notificacoesViewModel);
        }

        /// <summary>
        /// Conta notificações não lidas por usuário
        /// </summary>
        /// <param name="usuarioId">ID do usuário</param>
        /// <returns>Quantidade de notificações não lidas</returns>
        [HttpGet("usuario/{usuarioId}/count-nao-lidas")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> CountNaoLidasByUsuario([FromRoute] long usuarioId)
        {
            var count = _notificacaoService.ContarNaoLidasPorUsuario(usuarioId);
            return Ok(count);
        }

        /// <summary>
        /// Busca notificações por status
        /// </summary>
        /// <param name="status">Status da notificação</param>
        /// <returns>Lista de notificações com o status especificado</returns>
        [HttpGet("status/{status}")]
        [ProducesResponseType(typeof(IEnumerable<NotificacaoViewModel>), 200)]
        public ActionResult<IEnumerable<NotificacaoViewModel>> GetByStatus([FromRoute] string status)
        {
            var notificacoes = _notificacaoService.ObterNotificacoesPorStatus(status);
            var notificacoesViewModel = _mapper.Map<IEnumerable<NotificacaoViewModel>>(notificacoes);
            return Ok(notificacoesViewModel);
        }

        /// <summary>
        /// Busca notificações por tipo
        /// </summary>
        /// <param name="tipo">Tipo da notificação</param>
        /// <returns>Lista de notificações com o tipo especificado</returns>
        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(typeof(IEnumerable<NotificacaoViewModel>), 200)]
        public ActionResult<IEnumerable<NotificacaoViewModel>> GetByTipo([FromRoute] string tipo)
        {
            var notificacoes = _notificacaoService.ObterNotificacoesPorTipo(tipo);
            var notificacoesViewModel = _mapper.Map<IEnumerable<NotificacaoViewModel>>(notificacoes);
            return Ok(notificacoesViewModel);
        }

        /// <summary>
        /// Cria uma nova notificação
        /// </summary>
        /// <param name="viewModel">Dados da notificação</param>
        /// <returns>Notificação criada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(NotificacaoViewModel), 201)]
        [ProducesResponseType(400)]
        public ActionResult<NotificacaoViewModel> Post([FromBody] CreateNotificacaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notificacao = _mapper.Map<NotificacaoModel>(viewModel);
            _notificacaoService.AdicionarNotificacao(notificacao);

            var notificacaoViewModel = _mapper.Map<NotificacaoViewModel>(notificacao);
            return CreatedAtAction(nameof(GetById), new { id = notificacao.NotificacaoId }, notificacaoViewModel);
        }

        /// <summary>
        /// Atualiza uma notificação existente
        /// </summary>
        /// <param name="id">ID da notificação</param>
        /// <param name="viewModel">Dados atualizados da notificação</param>
        /// <returns>Notificação atualizada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(NotificacaoViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<NotificacaoViewModel> Put([FromRoute] long id, [FromBody] UpdateNotificacaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notificacaoExistente = _notificacaoService.ObterNotificacaoPorId(id);
            if (notificacaoExistente == null)
                return NotFound();

            _mapper.Map(viewModel, notificacaoExistente);
            _notificacaoService.AtualizarNotificacao(notificacaoExistente);

            var notificacaoViewModel = _mapper.Map<NotificacaoViewModel>(notificacaoExistente);
            return Ok(notificacaoViewModel);
        }

        /// <summary>
        /// Marca uma notificação como lida
        /// </summary>
        /// <param name="id">ID da notificação</param>
        /// <returns>Confirmação da marcação</returns>
        [HttpPatch("{id}/marcar-como-lida")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult MarcarComoLida([FromRoute] long id)
        {
            var notificacao = _notificacaoService.ObterNotificacaoPorId(id);
            if (notificacao == null)
                return NotFound();

            _notificacaoService.MarcarComoLida(id);
            return Ok();
        }

        /// <summary>
        /// Marca uma notificação como arquivada
        /// </summary>
        /// <param name="id">ID da notificação</param>
        /// <returns>Confirmação da marcação</returns>
        [HttpPatch("{id}/arquivar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult MarcarComoArquivada([FromRoute] long id)
        {
            var notificacao = _notificacaoService.ObterNotificacaoPorId(id);
            if (notificacao == null)
                return NotFound();

            _notificacaoService.MarcarComoArquivada(id);
            return Ok();
        }

        /// <summary>
        /// Remove uma notificação
        /// </summary>
        /// <param name="id">ID da notificação</param>
        /// <returns>Confirmação da remoção</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete([FromRoute] long id)
        {
            var notificacao = _notificacaoService.ObterNotificacaoPorId(id);
            if (notificacao == null)
                return NotFound();

            _notificacaoService.DeletarNotificacao(id);
            return NoContent();
        }
    }
}