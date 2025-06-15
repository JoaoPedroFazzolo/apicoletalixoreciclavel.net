using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace apicoletalixoreciclavel.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Obtém previsão do tempo (endpoint de teste)
    /// </summary>
    /// <returns>Lista de previsões do tempo</returns>
    /// <response code="200">Previsões obtidas com sucesso</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), 200)]
    public ActionResult<IEnumerable<WeatherForecast>> Get()
    {
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();

        return Ok(forecasts);
    }
}