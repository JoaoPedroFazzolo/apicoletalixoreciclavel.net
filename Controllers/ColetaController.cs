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
[Authorize(Roles = "Admin")]
public class ColetaController : Controller
{
    private readonly IMapper _mapper;
    private readonly IColetaService _service;

    public ColetaController(IMapper mapper, IColetaService coletaService)
    {
        _mapper = mapper;
        _service = coletaService;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<ColetaViewModel>> Get()
    {
        var coletas = _service.ObterTodasColetas();
        var viewModelList = _mapper.Map<IEnumerable<ColetaViewModel>>(coletas);
        return Ok(viewModelList);
    }

    [HttpGet("{id}")]
    public ActionResult<ColetaViewModel> GetById([FromRoute] long id)
    {
        var coleta = _service.ObterColetaPorId(id);
        if (coleta == null)
        {
            return NotFound();
        }
        var viewModel = _mapper.Map<ColetaViewModel>(coleta);
        return Ok(viewModel);
    }

    [HttpPost]
    public ActionResult Post([FromBody] CreateColetaViewModel viewModel)
    {
        var coleta = _mapper.Map<ColetaModel>(viewModel);
        _service.AdicionarColeta(coleta);
        return CreatedAtAction(nameof(Get), new { id = coleta.ColetaId }, viewModel);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] UpdateColetaViewModel viewModel)
    {
        var coleta = _service.ObterColetaPorId(id);
        if (coleta == null)
        {
            return NotFound();
        }
        _mapper.Map(viewModel, coleta);
        _service.AtualizarColeta(coleta);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] long id)
    {
        _service.DeletarColeta(id);
        return NoContent();
    }
}