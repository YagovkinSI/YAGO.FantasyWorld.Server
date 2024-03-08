using System;

namespace YAGO.FantasyWorld.Domain.Exceptions
{
    /// <summary>
    /// Ошибка приложения
    /// </summary>
    public class YagoException : Exception
    {
        public YagoException(string message, int errorCode = 500)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Код ошибки (HTTP)
        /// </summary>
        public int ErrorCode { get; }
    }
}
