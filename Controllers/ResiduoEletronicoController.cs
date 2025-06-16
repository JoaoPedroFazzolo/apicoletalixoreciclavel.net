using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apicoletalixoreciclavel.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ResiduoEletronicoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IResiduoEletronicoService _service;

    public ResiduoEletronicoController(IMapper mapper, IResiduoEletronicoService residuoEletronicoService)
    {
        _mapper = mapper;
        _service = residuoEletronicoService;
    }
    
    /// <summary>
    /// Obtém todos os resíduos eletrônicos
    /// </summary>
    /// <returns>Lista de resíduos eletrônicos</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ResiduoEletronicoViewModel>), 200)]
    public ActionResult<IEnumerable<ResiduoEletronicoViewModel>> Get()
    {
        var residuo = _service.ObterTodosResiduoEletronicos();
        var viewModelList = _mapper.Map<IEnumerable<ResiduoEletronicoViewModel>>(residuo);
        return Ok(viewModelList);
    }

    /// <summary>
    /// Obtém um resíduo eletrônico específico
    /// </summary>
    /// <param name="id">ID do resíduo eletrônico</param>
    /// <returns>Resíduo eletrônico encontrado</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResiduoEletronicoViewModel), 200)]
    [ProducesResponseType(404)]
    public ActionResult<ResiduoEletronicoViewModel> GetById([FromRoute] long id)
    {
        var residuo = _service.ObterResiduoEletronicoPorId(id);
        if (residuo == null)
        {
            return NotFound();
        }
        var viewModel = _mapper.Map<ResiduoEletronicoViewModel>(residuo);
        return Ok(viewModel);
    }

    /// <summary>
    /// Cria um novo resíduo eletrônico
    /// </summary>
    /// <param name="viewModel">Dados do resíduo eletrônico</param>
    /// <returns>Resíduo eletrônico criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ResiduoEletronicoViewModel), 201)]
    [ProducesResponseType(400)]
    public ActionResult Post([FromBody] CreateResiduoEletronicoViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var residuo = _mapper.Map<ResiduoEletronicoModel>(viewModel);
        _service.AdicionarResiduoEletronico(residuo);
        
        var responseViewModel = _mapper.Map<ResiduoEletronicoViewModel>(residuo);
        return CreatedAtAction(nameof(Get), new { id = residuo.ResiduoEletronicoId }, responseViewModel);
    }

    /// <summary>
    /// Atualiza um resíduo eletrônico existente
    /// </summary>
    /// <param name="id">ID do resíduo eletrônico</param>
    /// <param name="viewModel">Dados atualizados</param>
    /// <returns>Confirmação da atualização</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public ActionResult Put(long id, [FromBody] UpdateResiduoEletronicoViewModel viewModel) // CORRIGIDO: era int
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var residuo = _service.ObterResiduoEletronicoPorId(id);
        if (residuo == null)
        {
            return NotFound();
        }
        
        _mapper.Map(viewModel, residuo);
        _service.AtualizarResiduoEletronico(residuo);
        return NoContent();
    }

    /// <summary>
    /// Remove um resíduo eletrônico
    /// </summary>
    /// <param name="id">ID do resíduo eletrônico</param>
    /// <returns>Confirmação da remoção</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult Delete([FromRoute] long id)
    {
        var residuo = _service.ObterResiduoEletronicoPorId(id);
        if (residuo == null)
        {
            return NotFound();
        }

        _service.DeletarResiduoEletronico(id);
        return NoContent();
    }
}