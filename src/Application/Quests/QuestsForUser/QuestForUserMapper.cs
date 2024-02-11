using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Organizations;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Enums;
using YAGO.FantasyWorld.Server.Domain.Exceptions;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.ApplicationException;

namespace YAGO.FantasyWorld.Server.Application.Quests.QuestsForUser
{
    internal class QuestForUserMapper
    {
        private readonly OrganizationService _organizationService;

        public QuestForUserMapper(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public async Task<QuestForUser> GetQuestForUser(Quest quest, CancellationToken cancellationToken)
        {
            return quest.Type switch
            {
                QuestType.Unknown => throw new ApplicationException("Неизвестный тип квеста! Обратитесь к разработчику."),
                QuestType.BaseQuest => await GetBaseQuestForUser(quest, cancellationToken),
                _ => throw new NotImplementedApplicationException(),
            };
        }

        private async Task<QuestForUser> GetBaseQuestForUser(Quest quest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var organizationOpponent = await _organizationService.FindOrganization(quest.QuestEntity1Id, cancellationToken);

            return new QuestForUser
            {
                Id = quest.Id,
                OrganizationId = quest.OrganizationId,
                QuestText = $"Совет собрался, чтобы обсудить отношения с областью {organizationOpponent.Name}. " +
                    $"Некоторые члены совета предлагают сохранять нейтралитет и поддерживать мирные отношения. " +
                    $"Другие советуют искать взаимовыгодные сделки и укреплять экономическую связь. " +
                    $"Но есть и те, кто предлагает организовать набег с целью наживы и увеличения могущества.",
                QuestOptions = new[]
                {
                    "Сохранять нейтралитет и поддерживать мирные отношения.",
                    "Искать взаимовыгодные сделки и укреплять экономическую связь.",
                    "Организовать набег с целью наживы и увеличения могущества."
                },
                QuestOptionDescriptions = new[]
                {
                    "Полный успех (25%):\r\n\"" +
                        "Благодаря сохранению нейтралитета и поддержанию мирных отношений, Вы сумели успешно развивать свои владения.\"\r\n" +
                        "+15 могущества\r\n\r\n" +
                        "Ничего не происходит (70%):\r\n\"" +
                        "Ваши усилия по развитию владений идут не так быстро, как Вы ожидали.\"\r\n" +
                        "Ничего не происходит\r\n\r\n" +
                        "Неудача (5%):\r\n\"" +
                        "Излишняя изоляция и отсутствие активных отношений приводят к небольшому падению Вашего могущества.\"\r\n" +
                        "-5 могущества",
                    "Полный успех (10%):\r\n\"" +
                        "Укрепление экономической связи принесло Вам огромную прибыль, открывая новые возможности для развития владений.\"\r\n" +
                        "+24 могущества\r\n" +
                        "+12 могущества второй стороне\r\n\r\n" +
                        "Успех (35%):\r\n\"" +
                        "Сделка с партнером позволяет обоим сторонам получить значительные выгоды, способствуя процветанию владений.\"\r\n" +
                        "+8 могущества\r\n" +
                        "+8 могущества второй стороне\r\n\r\n" +
                        "Ничего не происходит (30%):\r\n\"" +
                        "Не смотря на Ваши старания, соглашение о сотрудничестве не удалось достичь, оставляя Вас без значительных изменений в развитии Ваших владений.\"\r\n" +
                        "Ничего не происходит\r\n\r\n" +
                        "Неудача (20%):\r\n\"" +
                        "Ваша сделка оказалась менее выгодной, чем ожидалось, и принесла некоторые убытки.\"\r\n" +
                        "-8 могущества\r\n" +
                        "+5 могущества второй стороне\r\n\r\n" +
                        "Полная неудача (5%):\r\n\"" +
                        "Торговый караван был разграблен разбойниками, причинив Вам значительный ущерб.\"\r\n" +
                        "-24 могущества",
                    "Полный успех (5%):\r\n\"" +
                        "Набег получился крайне успешным, вы возвращаетесь домой с огромной добычей.\"\r\n" +
                        "+120 могущества\r\n" +
                        "-70 могущества второй стороне\r\n\r\n" +
                        "Успех (50%):\r\n\"" +
                        "Набег прошёл успешно, добыча явно покроет все затраты на организацию набега.\"\r\n" +
                        "+40 могущества\r\n" +
                        "-20 могущества второй стороне\r\n\r\n" +
                        "Неудача (40%):\r\n\"" +
                        "Набег не увенчался успехом, понеся некоторые потери вы вернулись домой почти с пустыми руками.\"\r\n" +
                        "-40 могущества\r\n\r\n" +
                        "Полная неудача (5%):\r\n\"" +
                        "Это была ужасная идея, отряд попал в засаду и понёс ужасные потери\"\r\n" +
                        "-120 могущества"
                }
            };
        }
    }
}
