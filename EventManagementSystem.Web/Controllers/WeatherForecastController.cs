//using EventManagementSystem.Commons;
using EventManagementSystem.Commons;
using EventManagementSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private MyClass _class;
        private readonly EmContext _emContext;
        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            MyClass @class,
            EmContext emContext
        )
        {
            _logger = logger;
            _class = @class;
            _emContext = emContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable
                .Range(1, 5)
                .Select(
                    index =>
                        new WeatherForecast
                        {
                            Date = DateTime.Now.AddDays(index),
                            TemperatureC = Random.Shared.Next(-20, 55),
                            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                        }
                )
                .ToArray();
        }

        [HttpGet("test")]
        public string Test()
        {
            var response = _emContext.Events.First().Name;
            return response;
        }
    }
}
