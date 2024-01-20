using System;

namespace YAGO.FantasyWorld.Server.Host.Models.WeatherForecasts
{
    /// <summary>
    /// Прогноз погоды
    /// </summary>
    public class WeatherForecast
    {
        internal WeatherForecast(DateTimeOffset date, int temperatureC, string summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTimeOffset Date { get; }

        /// <summary>
        /// Температура в градусах Цельсия
        /// </summary>
        public int TemperatureC { get; }

        /// <summary>
        /// Температура в градусах Фаренгейта
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Общее описание
        /// </summary>
        public string Summary { get; }
    }
}
