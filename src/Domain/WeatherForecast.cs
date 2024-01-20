using System;

namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// Прогноз погоды
    /// </summary>
    public class WeatherForecast
    {
        public WeatherForecast(DateTimeOffset date, int temperature, string summary)
        {
            Date = date;
            Temperature = temperature;
            Summary = summary;
        }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTimeOffset Date { get; }

        /// <summary>
        /// Температура (в градусах Цельсия)
        /// </summary>
        public int Temperature { get; }

        /// <summary>
        /// Общее описание
        /// </summary>
        public string Summary { get; }
    }
}
