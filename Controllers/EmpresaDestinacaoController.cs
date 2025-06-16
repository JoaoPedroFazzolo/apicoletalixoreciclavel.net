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
    public class EmpresaDestinacaoController : ControllerBase
    {
        private readonly IEmpresaDestinacaoService _empresaDestinacaoService;
        private readonly IMapper _mapper;

        public EmpresaDestinacaoController(IEmpresaDestinacaoService empresaDestinacaoService, IMapper mapper)
        {
            _empresaDestinacaoService = empresaDestinacaoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todas as empresas de destinação
        /// </summary>
        /// <returns>Lista de empresas de destinação</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmpresaDestinacaoViewModel>), 200)]
        public ActionResult<IEnumerable<EmpresaDestinacaoViewModel>> GetAll()
        {
            var empresas = _empresaDestinacaoService.ObterTodasEmpresas();
            var empresasViewModel = _mapper.Map<IEnumerable<EmpresaDestinacaoViewModel>>(empresas);
            return Ok(empresasViewModel);
        }

        /// <summary>
        /// Busca uma empresa de destinação por ID
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <returns>Empresa encontrada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmpresaDestinacaoViewModel), 200)]
        [ProducesResponseType(404)]
        public ActionResult<EmpresaDestinacaoViewModel> GetById([FromRoute] long id)
        {
            var empresa = _empresaDestinacaoService.ObterEmpresaPorId(id);
            if (empresa == null)
                return NotFound();

            var empresaViewModel = _mapper.Map<EmpresaDestinacaoViewModel>(empresa);
            return Ok(empresaViewModel);
        }

        /// <summary>
        /// Pesquisa empresas por nome
        /// </summary>
        /// <param name="nome">Nome para pesquisa</param>
        /// <returns>Lista de empresas encontradas</returns>
        [HttpGet("pesquisar/{nome}")]
        [ProducesResponseType(typeof(IEnumerable<EmpresaDestinacaoViewModel>), 200)]
        public ActionResult<IEnumerable<EmpresaDestinacaoViewModel>> SearchByName([FromRoute] string nome)
        {
            var empresas = _empresaDestinacaoService.PesquisarPorNome(nome);
            var empresasViewModel = _mapper.Map<IEnumerable<EmpresaDestinacaoViewModel>>(empresas);
            return Ok(empresasViewModel);
        }

        /// <summary>
        /// Cria uma nova empresa de destinação
        /// </summary>
        /// <param name="viewModel">Dados da empresa</param>
        /// <returns>Empresa criada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmpresaDestinacaoViewModel), 201)]
        [ProducesResponseType(400)]
        public ActionResult<EmpresaDestinacaoViewModel> Post([FromBody] CreateEmpresaDestinacaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var empresa = _mapper.Map<EmpresaDestinacaoModel>(viewModel);
                _empresaDestinacaoService.AdicionarEmpresa(empresa);

                var empresaViewModel = _mapper.Map<EmpresaDestinacaoViewModel>(empresa);
                return CreatedAtAction(nameof(GetById), new { id = empresa.EmpresaDestinacaoId }, empresaViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma empresa de destinação existente
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <param name="viewModel">Dados atualizados da empresa</param>
        /// <returns>Empresa atualizada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmpresaDestinacaoViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<EmpresaDestinacaoViewModel> Put([FromRoute] long id, [FromBody] UpdateEmpresaDestinacaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var empresaExistente = _empresaDestinacaoService.ObterEmpresaPorId(id);
                if (empresaExistente == null)
                    return NotFound();

                _mapper.Map(viewModel, empresaExistente);
                _empresaDestinacaoService.AtualizarEmpresa(empresaExistente);

                var empresaViewModel = _mapper.Map<EmpresaDestinacaoViewModel>(empresaExistente);
                return Ok(empresaViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma empresa de destinação
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <returns>Confirmação da remoção</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete([FromRoute] long id)
        {
            try
            {
                _empresaDestinacaoService.DeletarEmpresa(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}