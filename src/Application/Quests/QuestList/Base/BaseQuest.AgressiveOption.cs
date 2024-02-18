using YAGO.FantasyWorld.Server.Domain.Quests;

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
                    GetAgressiveResult1(5,quest),
                    GetAgressiveResult2(50, quest),
                    GetAgressiveResult3(40, quest),
                    GetAgressiveResult4(5, quest)
                }
            );
        }

        private static QuestOptionResult GetAgressiveResult1(int weight, Quest quest)
        {
            return new(
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
                "Это была ужасная идея, отряд попал в засаду и понёс ужасные потери.",
                weight,
                new[] {
                    CreateChangeOrganization(quest.OrganizationId, -120)
                }
            );
        }
    }
}
