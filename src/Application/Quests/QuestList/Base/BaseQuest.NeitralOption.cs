using System;
using YAGO.FantasyWorld.Server.Domain.Common;
using YAGO.FantasyWorld.Server.Domain.Enums;
using YAGO.FantasyWorld.Server.Domain.Quests;

namespace YAGO.FantasyWorld.Server.Application.Quests.QuestList.Base
{
    internal partial class BaseQuest
    {
        private static QuestOption GetNeitralOption(Quest quest)
        {
            return new(
                (int)BaseQuestOptionType.Neitral,
                "Сохранять нейтралитет и поддерживать мирные отношения.",
                new QuestOptionResult[]
                {
                    GetNeitralResult1(25, quest),
                    GetNeitralResult2(70),
                    GetNeitralResult3(15, quest)
                }
            );
        }

        private static QuestOptionResult GetNeitralResult1(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.CriticalSuccess,
                "Благодаря сохранению нейтралитета и поддержанию мирных отношений, Вы сумели успешно развивать свои владения.",
                weight,
                new[] { CreateChangeOrganization(quest.OrganizationId, 15) }
            );
        }

        private static QuestOptionResult GetNeitralResult2(int weight)
        {
            return new(
                QuestOptionResultType.Neitral,
                "Ваши усилия по развитию владений идут не так быстро, как Вы ожидали.",
                weight,
                Array.Empty<EntityChange>()
            );
        }

        private static QuestOptionResult GetNeitralResult3(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.Fail,
                "Излишняя изоляция и отсутствие активных отношений приводят к небольшому падению Вашего могущества.",
                weight,
                new[] { CreateChangeOrganization(quest.OrganizationId, -5) }
            );
        }
    }
}
