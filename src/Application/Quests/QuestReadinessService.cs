using System;
using YAGO.FantasyWorld.Domain;

namespace YAGO.FantasyWorld.Server.Application.Quests
{
    /// <summary>
    /// Сервис проверки готовности квеста
    /// </summary>
    public class QuestReadinessService
    {

        private readonly TimeSpan[] QUEST_TIMEOUTS = new[]
        {
            TimeSpan.FromMinutes(2),
            TimeSpan.FromMinutes(5),
            TimeSpan.FromMinutes(10),
            TimeSpan.FromMinutes(20),
            TimeSpan.FromMinutes(40),
            TimeSpan.FromMinutes(90),
            TimeSpan.FromHours(3)
        };
        public int QuestTimeoutCount => QUEST_TIMEOUTS.Length;

        public DateTimeOffset CalcQuestReadyDateTime(Quest[] lastQuests)
        {
            if (lastQuests.Length < QUEST_TIMEOUTS.Length)
                return DateTimeOffset.MinValue;

            var questReadyDateTime = DateTimeOffset.Now + QUEST_TIMEOUTS[0];
            for (var i = 0; i < QUEST_TIMEOUTS.Length; i++)
            {
                var currentQuestReadyDateTime = lastQuests[i].Created + QUEST_TIMEOUTS[i];
                if (currentQuestReadyDateTime < questReadyDateTime)
                    questReadyDateTime = currentQuestReadyDateTime;
                if (currentQuestReadyDateTime < DateTimeOffset.Now)
                    break;
            }

            return questReadyDateTime;
        }
    }
}
