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
    public class AlertaController : ControllerBase
    {
        private readonly IAlertaService _alertaService;
        private readonly IMapper _mapper;

        public AlertaController(IAlertaService alertaService, IMapper mapper)
        {
            _alertaService = alertaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos os alertas
        /// </summary>
        /// <returns>Lista de alertas</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlertaViewModel>), 200)]
        public ActionResult<IEnumerable<AlertaViewModel>> Get([FromQuery] PaginationRequest pagination)
        {
            var alertas = _alertaService.ObterTodosAlertas(pagination.PageNumber, pagination.PageSize);
            var alertasViewModel = _mapper.Map<IEnumerable<AlertaViewModel>>(alertas);
            return Ok(alertasViewModel);
        }

        /// <summary>
        /// Busca um alerta por ID
        /// </summary>
        /// <param name="id">ID do alerta</param>
        /// <returns>Alerta encontrado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AlertaViewModel), 200)]
        [ProducesResponseType(404)]
        public ActionResult<AlertaViewModel> GetById([FromRoute] long id)
        {
            var alerta = _alertaService.ObterAlertaPorId(id);
            
            if (alerta == null)
                return NotFound();

            var alertaViewModel = _mapper.Map<AlertaViewModel>(alerta);
            return Ok(alertaViewModel);
        }

        /// <summary>
        /// Busca alertas por status
        /// </summary>
        /// <param name="status">Status do alerta</param>
        /// <returns>Lista de alertas com o status especificado</returns>
        [HttpGet("status/{status}")]
        [ProducesResponseType(typeof(IEnumerable<AlertaViewModel>), 200)]
        public ActionResult<IEnumerable<AlertaViewModel>> GetByStatus([FromRoute] string status)
        {
            var alertas = _alertaService.ObterAlertasPorStatus(status);
            var alertasViewModel = _mapper.Map<IEnumerable<AlertaViewModel>>(alertas);
            return Ok(alertasViewModel);
        }

        /// <summary>
        /// Busca alertas por tipo
        /// </summary>
        /// <param name="tipo">Tipo do alerta</param>
        /// <returns>Lista de alertas do tipo especificado</returns>
        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(typeof(IEnumerable<AlertaViewModel>), 200)]
        public ActionResult<IEnumerable<AlertaViewModel>> GetByTipo([FromRoute] string tipo)
        {
            var alertas = _alertaService.ObterAlertasPorTipo(tipo);
            var alertasViewModel = _mapper.Map<IEnumerable<AlertaViewModel>>(alertas);
            return Ok(alertasViewModel);
        }

        /// <summary>
        /// Busca alertas por usuário
        /// </summary>
        /// <param name="usuarioId">ID do usuário</param>
        /// <returns>Lista de alertas do usuário</returns>
        [HttpGet("usuario/{usuarioId}")]
        [ProducesResponseType(typeof(IEnumerable<AlertaViewModel>), 200)]
        public ActionResult<IEnumerable<AlertaViewModel>> GetByUsuario([FromRoute] long usuarioId)
        {
            var alertas = _alertaService.ObterAlertasPorUsuario(usuarioId);
            var alertasViewModel = _mapper.Map<IEnumerable<AlertaViewModel>>(alertas);
            return Ok(alertasViewModel);
        }

        /// <summary>
        /// Cria um novo alerta
        /// </summary>
        /// <param name="viewModel">Dados do alerta</param>
        /// <returns>Alerta criado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AlertaViewModel), 201)]
        [ProducesResponseType(400)]
        public ActionResult<AlertaViewModel> Post([FromBody] CreateAlertaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var alerta = _mapper.Map<AlertaModel>(viewModel);
            _alertaService.AdicionarAlerta(alerta);

            var alertaViewModel = _mapper.Map<AlertaViewModel>(alerta);
            return CreatedAtAction(nameof(GetById), new { id = alerta.AlertaId }, alertaViewModel);
        }

        /// <summary>
        /// Atualiza um alerta existente
        /// </summary>
        /// <param name="id">ID do alerta</param>
        /// <param name="viewModel">Dados atualizados do alerta</param>
        /// <returns>Alerta atualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AlertaViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<AlertaViewModel> Put([FromRoute] long id, [FromBody] UpdateAlertaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var alertaExistente = _alertaService.ObterAlertaPorId(id);
            if (alertaExistente == null)
                return NotFound();

            _mapper.Map(viewModel, alertaExistente);
            _alertaService.AtualizarAlerta(alertaExistente);

            var alertaViewModel = _mapper.Map<AlertaViewModel>(alertaExistente);
            return Ok(alertaViewModel);
        }

        /// <summary>
        /// Atualiza apenas o status de um alerta
        /// </summary>
        /// <param name="id">ID do alerta</param>
        /// <param name="status">Novo status</param>
        /// <returns>Confirmação da atualização</returns>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult UpdateStatus([FromRoute] long id, [FromBody] string status)
        {
            var alerta = _alertaService.ObterAlertaPorId(id);
            if (alerta == null)
                return NotFound();

            _alertaService.AtualizarStatusAlerta(id, status);
            return Ok();
        }

        /// <summary>
        /// Remove um alerta
        /// </summary>
        /// <param name="id">ID do alerta</param>
        /// <returns>Confirmação da remoção</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete([FromRoute] long id)
        {
            var alerta = _alertaService.ObterAlertaPorId(id);
            if (alerta == null)
                return NotFound();

            _alertaService.DeletarAlerta(id);
            return NoContent();
        }

        /// <summary>
        /// Conta alertas por status
        /// </summary>
        /// <param name="status">Status para contar</param>
        /// <returns>Número de alertas com o status</returns>
        [HttpGet("count/{status}")]
        [ProducesResponseType(typeof(long), 200)]
        public ActionResult<long> CountByStatus([FromRoute] string status)
        {
            var count = _alertaService.ContarAlertasPorStatus(status);
            return Ok(count);
        }
    }
}