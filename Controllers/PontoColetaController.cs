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
public class PontoColetaController : Controller
{
    private readonly IMapper _mapper;
    private readonly IPontoColetaService _service;

    public PontoColetaController(IMapper mapper, IPontoColetaService pontoColetaService)
    {
        _mapper = mapper;
        _service = pontoColetaService;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<PontoColetaViewModel>> Get()
    {
        var pontos = _service.ObterTodosPontosColetas();
        var viewModelList = _mapper.Map<IEnumerable<PontoColetaViewModel>>(pontos);
        return Ok(viewModelList);
    }

    [HttpGet("{id}")]
    public ActionResult<PontoColetaViewModel> GetById([FromRoute] long id)
    {
        var ponto = _service.ObterPontoColetaPorId(id);
        if (ponto == null)
        {
            return NotFound();
        }
        var viewModel = _mapper.Map<PontoColetaViewModel>(ponto);
        return Ok(viewModel);
    }

    [HttpPost]
    public ActionResult Post([FromBody] CreatePontoColetaViewModel viewModel)
    {
        var ponto = _mapper.Map<PontoColetaModel>(viewModel);
        _service.AdicionarPontoColeta(ponto);
        return CreatedAtAction(nameof(Get), new { id = ponto.PontoColetaId }, viewModel);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] UpdatePontoColetaViewModel viewModel)
    {
        var ponto = _service.ObterPontoColetaPorId(id);
        if (ponto == null)
        {
            return NotFound();
        }
        _mapper.Map(viewModel, ponto);
        _service.AtualizarPontoColeta(ponto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] long id)
    {
        _service.DeletarPontoColeta(id);
        return NoContent();
    }
}