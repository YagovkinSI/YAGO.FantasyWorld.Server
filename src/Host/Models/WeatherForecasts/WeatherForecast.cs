using System;

namespace YAGO.FantasyWorld.Server.Host.Models.WeatherForecasts
{
    /// <summary>
    /// ������� ������
    /// </summary>
    public class WeatherForecast
    {
        internal WeatherForecast(DateTime date, int temperatureC, string summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }

        /// <summary>
        /// ����
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// ����������� � �������� �������
        /// </summary>
        public int TemperatureC { get; }

        /// <summary>
        /// ����������� � �������� ����������
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// ����� ��������
        /// </summary>
        public string Summary { get; }
    }
}
