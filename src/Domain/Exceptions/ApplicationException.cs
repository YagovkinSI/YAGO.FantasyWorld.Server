using System;

namespace YAGO.FantasyWorld.Server.Domain.Exceptions
{
    /// <summary>
    /// Ошибка приложения
    /// </summary>
    public class ApplicationException : Exception
    {
        public ApplicationException(string message, int errorCode = 500)
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
