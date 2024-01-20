using System;

namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
	/// Пользователь
	/// </summary>
	public class User
    {
        public User(string id, string name, DateTimeOffset registration, DateTimeOffset lastActivity)
        {
            Id = id;
            Name = name;
            Registration = registration;
            LastActivity = lastActivity;
        }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата и время регистрации пользователя в системе
        /// </summary>
        public DateTimeOffset Registration { get; set; }

        /// <summary>
        /// Дата и время последней активности пользователья в системе
        /// </summary>
        public DateTimeOffset LastActivity { get; set; }
    }
}
