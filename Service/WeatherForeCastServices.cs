namespace AspNetBeginner.Service
{
   /* public class WeatherForeCastServices //: IWeatherForeCastServices
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ILogger<WeatherForeCastServices> _logger;

        public WeatherForeCastServices(ILogger<WeatherForeCastServices> logger)
        {
            _logger = logger;
        }
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            _logger.LogInformation("Getting forecasts data");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }*/
}
