using AspNetBeginner.Service;
using Microsoft.AspNetCore.Mvc;

namespace AspNetBeginner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        private readonly ILogger<WeatherForecastController> _logger;
       // private readonly IWeatherForeCastServices _services;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)//,//IWeatherForeCastServices services)
        {
            _logger = logger;
           // this._services = services;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // var service = new WeatherForeCastServices(new Logger<WeatherForeCastServices>(new LoggerFactory()));
            //return _services.GetForecasts();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
           .ToArray();
        }
    }
}
