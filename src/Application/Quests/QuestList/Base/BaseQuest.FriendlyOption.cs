using System;
using Yago.FantasyWorld.ApiContracts.Common.Models;
using Yago.FantasyWorld.ApiContracts.Domain;
using Yago.FantasyWorld.ApiContracts.QuestApi.Enums;
using Yago.FantasyWorld.ApiContracts.QuestApi.Models;

namespace YAGO.FantasyWorld.Server.Application.Quests.QuestList.Base
{
    internal partial class BaseQuest
    {
        private static QuestOption GetFriendlyOption(Quest quest)
        {
            return new(
                (int)BaseQuestOptionType.Friendly,
                "Искать взаимовыгодные сделки и укреплять экономическую связь.",
                new QuestOptionResult[]
                {
                    GetFriendlyResult1(10, quest),
                    GetFriendlyResult2(35, quest),
                    GetFriendlyResult3(30),
                    GetFriendlyResult4(20, quest),
                    GetFriendlyResult5(5, quest)
                }
            );
        }

        private static QuestOptionResult GetFriendlyResult1(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.CriticalSuccess,
                "Укрепление экономической связи принесло Вам огромную прибыль, открывая новые возможности для развития владений.",
                weight,
                new[] {
                    CreateChangeOrganization(quest.OrganizationId, 24),
                    CreateChangeOrganization(quest.QuestEntity1Id, 12)
                }
            );
        }

        private static QuestOptionResult GetFriendlyResult2(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.Success,
                "Сделка с партнером позволяет обоим сторонам получить значительные выгоды, способствуя процветанию владений.",
                weight,
                new[] {
                    CreateChangeOrganization(quest.OrganizationId, 8),
                    CreateChangeOrganization(quest.QuestEntity1Id, 8)
                }
            );
        }

        private static QuestOptionResult GetFriendlyResult3(int weight)
        {
            return new(
                QuestOptionResultType.Neitral,
                "Не смотря на Ваши старания, соглашение о сотрудничестве не удалось достичь, оставляя Вас без значительных изменений в развитии Ваших владений.",
                weight,
                Array.Empty<EntityChange>()
            );
        }

        private static QuestOptionResult GetFriendlyResult4(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.Fail,
                "Ваша сделка оказалась менее выгодной, чем ожидалось, и принесла некоторые убытки.",
                weight,
                new[] {
                    CreateChangeOrganization(quest.OrganizationId, -8),
                    CreateChangeOrganization(quest.QuestEntity1Id, 5)
                }
            );
        }

        private static QuestOptionResult GetFriendlyResult5(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.CriticalFail,
                "Торговый караван был разграблен разбойниками, причинив Вам значительный ущерб.",
                weight,
                new[] { CreateChangeOrganization(quest.OrganizationId, -24) }
            );
        }
    }
}
