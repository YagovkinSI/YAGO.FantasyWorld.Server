using System;
using YAGO.FantasyWorld.Server.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Domain
{
    /// <summary>
    /// Данные квеста для пользователя
    /// </summary>
    public class QuestForUser
    {
        /// <summary>
        /// Идентификатор квеста
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентифкатор организации
        /// </summary>
        public long OrganizationId { get; set; }

        /// <summary>
        /// Текст квеста
        /// </summary>
        public string QuestText { get; set; }

        /// <summary>
        /// Варианты решения
        /// </summary>
        public string[] QuestOptions { get; set; }

        /// <summary>
        /// Подробности вариантов решения
        /// </summary>
        public string[] QuestOptionDescriptions { get; set; }

    }
}
