using YAGO.FantasyWorld.ApiContracts.QuestApi.Enums;
using YAGO.FantasyWorld.Domain;
using YAGO.FantasyWorld.ApiContracts.QuestApi.Models;

namespace YAGO.FantasyWorld.Server.Application.Quests.QuestList.Base
{
    internal partial class BaseQuest
    {
        private static QuestOption GetAgressiveOption(Quest quest)
        {
            return new(
                (int)BaseQuestOptionType.Agressive,
                "Организовать набег с целью наживы и увеличения могущества.",
                new QuestOptionResult[]
                {
                    GetAgressiveResult1(5, quest),
                    GetAgressiveResult2(50, quest),
                    GetAgressiveResult3(40, quest),
                    GetAgressiveResult4(5, quest)
                }
            );
        }

        private static QuestOptionResult GetAgressiveResult1(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.CriticalSuccess,
                "Набег получился крайне успешным, вы возвращаетесь домой с огромной добычей.",
                weight,
                new[] {
                    CreateChangeOrganization(quest.OrganizationId, 120),
                    CreateChangeOrganization(quest.QuestEntity1Id, -70)
                }
            );
        }

        private static QuestOptionResult GetAgressiveResult2(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.Success,
                "Набег прошёл успешно, добыча явно покроет все затраты на организацию набега.",
                weight,
                new[] {
                    CreateChangeOrganization(quest.OrganizationId, 40),
                    CreateChangeOrganization(quest.QuestEntity1Id, -20)
                }
            );
        }

        private static QuestOptionResult GetAgressiveResult3(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.Fail,
                "Набег не увенчался успехом, понеся некоторые потери вы вернулись домой почти с пустыми руками.",
                weight,
                new[] {
                    CreateChangeOrganization(quest.OrganizationId, -40)
                }
            );
        }

        private static QuestOptionResult GetAgressiveResult4(int weight, Quest quest)
        {
            return new(
                QuestOptionResultType.CriticalFail,
                "Это была ужасная идея, отряд попал в засаду и понёс ужасные потери.",
                weight,
                new[] {
                    CreateChangeOrganization(quest.OrganizationId, -120)
                }
            );
        }
    }
}
