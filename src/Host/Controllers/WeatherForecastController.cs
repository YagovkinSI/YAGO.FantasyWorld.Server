using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.WeatherForecastService;
using YAGO.FantasyWorld.Server.Host.Models.WeatherForecasts;

namespace YAGO.FantasyWorld.Server.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController(WeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var weatherForecasts = await _weatherForecastService.GetWeatherForecastList(cancellationToken);
            return weatherForecasts
                .Select(w => ToApi(w));
        }

        private static WeatherForecast ToApi(Domain.WeatherForecast weatherForecast)
        {
            return new WeatherForecast
            (
                weatherForecast.Date,
                weatherForecast.Temperature,
                weatherForecast.Summary
            );
        }
    }
}
