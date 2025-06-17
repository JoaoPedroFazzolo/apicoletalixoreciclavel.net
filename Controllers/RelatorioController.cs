using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apicoletalixoreciclavel.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Authorize(Roles = "Admin")]
public class RelatorioController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRelatorioService _service;

    public RelatorioController(IMapper mapper, IRelatorioService relatorioService)
    {
        _mapper = mapper;
        _service = relatorioService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(RelatorioViewModel), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult Post([FromBody] CreateRelatorioViewModel createRelatorioViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var relatorio = _mapper.Map<RelatorioModel>(createRelatorioViewModel);

        try
        {
            _service.CriarRelatorio(relatorio);
            var relatorioViewModel = _mapper.Map<RelatorioViewModel>(relatorio);
            return CreatedAtAction(nameof(GetRelatorioById), new { id = relatorio.RelatorioId }, relatorioViewModel);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RelatorioViewModel), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<RelatorioViewModel> GetRelatorioById(long id)
    {
        if (id <= 0)
        {
            return BadRequest("ID deve ser maior que zero");
        }

        try
        {
            var relatorio = _service.ObterRelatorioPorId(id);
            if (relatorio == null)
            {
                return NotFound($"Relatório com ID {id} não encontrado");
            }

            var relatorioViewModel = _mapper.Map<RelatorioViewModel>(relatorio);
            return Ok(relatorioViewModel);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RelatorioViewModel>), 200)]
    [ProducesResponseType(500)]
    public ActionResult<IEnumerable<RelatorioViewModel>> GetAll()
    {
        try
        {
            var relatorios = _service.ObterTodosRelatorios();
            var relatoriosViewModel = _mapper.Map<IEnumerable<RelatorioViewModel>>(relatorios);
            return Ok(relatoriosViewModel);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor");
        }
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RelatorioViewModel), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<RelatorioViewModel> Put(long id, [FromBody] UpdateRelatorioViewModel updateRelatorioViewModel)
    {
        if (id <= 0)
        {
            return BadRequest("ID deve ser maior que zero");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var relatorioExistente = _service.ObterRelatorioPorId(id);
            if (relatorioExistente == null)
            {
                return NotFound($"Relatório com ID {id} não encontrado");
            }
            
            _mapper.Map(updateRelatorioViewModel, relatorioExistente);

            _service.AtualizarRelatorio(relatorioExistente);

            var relatorioViewModel = _mapper.Map<RelatorioViewModel>(relatorioExistente);
            return Ok(relatorioViewModel);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult Delete(long id)
    {
        if (id <= 0)
        {
            return BadRequest("ID deve ser maior que zero");
        }

        try
        {
            var relatorioExistente = _service.ObterRelatorioPorId(id);
            if (relatorioExistente == null)
            {
                return NotFound($"Relatório com ID {id} não encontrado");
            }

            _service.DeletarRelatorio(relatorioExistente);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno do servidor");
        }
    }

}