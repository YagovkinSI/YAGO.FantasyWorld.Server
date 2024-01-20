using System;

namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// ������� ������
    /// </summary>
    public class WeatherForecast
    {
        public WeatherForecast(DateTime date, int temperature, string summary)
        {
            Date = date;
            Temperature = temperature;
            Summary = summary;
        }

        /// <summary>
        /// ����
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// ����������� (� �������� �������)
        /// </summary>
        public int Temperature { get; }

        /// <summary>
        /// ����� ��������
        /// </summary>
        public string Summary { get; }
    }
}
