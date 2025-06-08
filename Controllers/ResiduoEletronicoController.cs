using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apicoletalixoreciclavel.Controllers;
[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class ResiduoEletronicoController : Controller
{
    private readonly IMapper _mapper;
    private readonly IResiduoEletronicoService _service;

    public ResiduoEletronicoController(IMapper mapper, IResiduoEletronicoService residuoEletronicoService)
    {
        _mapper = mapper;
        _service = residuoEletronicoService;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<ResiduoEletronicoViewModel>> Get()
    {
        var residuo = _service.ObterTodosResiduoEletronicos();
        var viewModelList = _mapper.Map<IEnumerable<ResiduoEletronicoViewModel>>(residuo);
        return Ok(viewModelList);
    }

    [HttpGet("{id}")]
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

    [HttpPost]
    public ActionResult Post([FromBody] ResiduoEletronicoViewModel viewModel)
    {
        var residuo = _mapper.Map<ResiduoEletronicoModel>(viewModel);
        _service.AdicionarResiduoEletronico(residuo);
        return CreatedAtAction(nameof(Get), new { id = residuo.ResiduoEletronicoId }, viewModel);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] ResiduoEletronicoViewModel viewModel)
    {
        var residuo = _service.ObterResiduoEletronicoPorId(id);
        if (residuo == null)
        {
            return NotFound();
        }
        _mapper.Map(viewModel, residuo);
        _service.AtualizarResiduoEletronico(residuo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] long id)
    {
        _service.DeletarResiduoEletronico(id);
        return NoContent();
    }
}