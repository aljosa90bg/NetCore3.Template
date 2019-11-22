using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore3.Template.Api.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace NetCore3.Template.Api.Controllers
{
    [ApiVersion(Versions.Version1)]
    [Produces("application/json")]
    [ApiController]
    [Route(Routes.WeatherForecastBase)]
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

        [HttpGet]
        [ApiVersion(Versions.Version1)]
        [SwaggerResponse(StatusCodes.Status200OK, DefaultRouteDescriptions.GetSuccessfulResponse, typeof(IEnumerable<WeatherForecast>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, DefaultRouteDescriptions.NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, DefaultRouteDescriptions.BadRequest, typeof(IEnumerable<ValidationFailure>))]
        // TODO: Create NotFoundException class
        //[SwaggerResponse(StatusCodes.Status404NotFound, DefaultRouteDescriptions.NotFound, typeof(IEnumerable<NotFoundException>))]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
