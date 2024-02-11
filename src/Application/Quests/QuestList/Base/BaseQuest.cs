using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Organizations;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Enums;
using YAGO.FantasyWorld.Server.Domain.Exceptions;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.ApplicationException;

namespace YAGO.FantasyWorld.Server.Application.Quests.QuestList.Base
{
    internal partial class BaseQuest : IQuestDetails
    {
        private readonly OrganizationService _organizationService;

        public BaseQuest(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        public QuestType Type => QuestType.BaseQuest;

        public int CountOptions => Options.Length;

        private readonly QuestOption _optionNeitral = new QuestOption(
            "Сохранять нейтралитет и поддерживать мирные отношения.",
            new[]
            {
                new QuestOptionResult("Благодаря сохранению нейтралитета и поддержанию мирных отношений, Вы сумели успешно развивать свои владения.",
                    25, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Ваши усилия по развитию владений идут не так быстро, как Вы ожидали.",
                    70, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Излишняя изоляция и отсутствие активных отношений приводят к небольшому падению Вашего могущества.",
                    5, () => throw new NotImplementedApplicationException())
            });

        private readonly QuestOption _optionFriendly = new QuestOption(
            "Искать взаимовыгодные сделки и укреплять экономическую связь.",
            new[]
            {
                new QuestOptionResult("Укрепление экономической связи принесло Вам огромную прибыль, открывая новые возможности для развития владений.",
                    10, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Сделка с партнером позволяет обоим сторонам получить значительные выгоды, способствуя процветанию владений.",
                    35, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Не смотря на Ваши старания, соглашение о сотрудничестве не удалось достичь, оставляя Вас без значительных изменений в развитии Ваших владений.",
                    30, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Ваша сделка оказалась менее выгодной, чем ожидалось, и принесла некоторые убытки.",
                    20, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Торговый караван был разграблен разбойниками, причинив Вам значительный ущерб.",
                    5, () => throw new NotImplementedApplicationException())
            });

        private readonly QuestOption _optionAgressive = new QuestOption(
            "Организовать набег с целью наживы и увеличения могущества.",
            new[]
            {
                new QuestOptionResult("Набег получился крайне успешным, вы возвращаетесь домой с огромной добычей.",
                    5, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Набег прошёл успешно, добыча явно покроет все затраты на организацию набега.",
                    50, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Набег не увенчался успехом, понеся некоторые потери вы вернулись домой почти с пустыми руками.",
                    40, () => throw new NotImplementedApplicationException()),
                new QuestOptionResult("Это была ужасная идея, отряд попал в засаду и понёс ужасные потери.",
                    5, () => throw new NotImplementedApplicationException())
            });

        public QuestOption[] Options => new[]
        {
            _optionNeitral,
            _optionFriendly,
            _optionAgressive
        };

        public async Task<QuestForUser> GetQuestForUser(Quest quest, CancellationToken cancellationToken)
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
                QuestOptions = Options.Select(o => o.Text).ToArray(),
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

        public async Task<string> HandleQuestOption(int questOptionIndex, CancellationToken cancellationToken)
        {
            if (CountOptions <= questOptionIndex)
                throw new ApplicationException("Неверный индекс варианта решения квеста.");

            var option = Options[questOptionIndex];
            var sumWeight = option.QuestOptionResults.Sum(r => r.Weight);
            var random = new Random().Next(1, sumWeight);

            QuestOptionResult result = option.QuestOptionResults[0];
            var index = 0;
            while (random > 0)
            {
                result = option.QuestOptionResults[index];
                random -= result.Weight;
            }

            return await result.Handle.Invoke();
        }
    }
}
