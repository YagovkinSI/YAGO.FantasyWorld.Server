using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Organizations;
using YAGO.FantasyWorld.Server.Domain.Enums;
using YAGO.FantasyWorld.Server.Domain.Quests;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.ApplicationException;

namespace YAGO.FantasyWorld.Server.Application.Quests.QuestList.Base
{
    internal partial class BaseQuest : IQuestDetails
    {
        private readonly OrganizationService _organizationService;

        public BaseQuest(Quest quest, OrganizationService organizationService)
        {
            Quest = quest;
            _organizationService = organizationService;
        }

        public Quest Quest { get; }

        public QuestType Type => QuestType.BaseQuest;

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
                QuestOptions = GetQuestOptions(quest)
            };
        }

        private static QuestOption[] GetQuestOptions(Quest quest)
        {
            return new QuestOption[]
            {
                new (
                    (int)BaseQuestOptionType.Neitral,
                    "Сохранять нейтралитет и поддерживать мирные отношения.",
                    new QuestOptionResult[]
                    {
                        new (
                            "Благодаря сохранению нейтралитета и поддержанию мирных отношений, Вы сумели успешно развивать свои владения.",
                            25,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, 15)
                                    }
                                )
                            }
                        ),
                        new (
                            "Ваши усилия по развитию владений идут не так быстро, как Вы ожидали..",
                            70,
                            new QuestOptionResultEntity[] {}
                        ),
                        new (
                            "Излишняя изоляция и отсутствие активных отношений приводят к небольшому падению Вашего могущества.",
                            5,
                            new QuestOptionResultEntity[]
                            {
                                new                                    (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, -5)
                                    }
                                )
                            }
                        ),
                    }
                ),
                new (
                    (int)BaseQuestOptionType.Friendly,
                    "Искать взаимовыгодные сделки и укреплять экономическую связь.",
                    new QuestOptionResult[]
                    {
                        new (
                            "Укрепление экономической связи принесло Вам огромную прибыль, открывая новые возможности для развития владений.",
                            10,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, 24)
                                    }
                                ),
                                new (
                                    EntityType.Organization,
                                    quest.QuestEntity1Id,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, 12)
                                    }
                                )
                            }
                        ),
                        new (
                            "Сделка с партнером позволяет обоим сторонам получить значительные выгоды, способствуя процветанию владений.",
                            35,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, 8)
                                    }
                                ),
                                new (
                                    EntityType.Organization,
                                    quest.QuestEntity1Id,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, 8)
                                    }
                                )
                            }
                        ),
                        new (
                            "Не смотря на Ваши старания, соглашение о сотрудничестве не удалось достичь, оставляя Вас без значительных изменений в развитии Ваших владений.",
                            30,
                            new QuestOptionResultEntity[] { }
                        ),
                        new (
                            "Ваша сделка оказалась менее выгодной, чем ожидалось, и принесла некоторые убытки.",
                            20,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, -8)
                                    }
                                ),
                                new (
                                    EntityType.Organization,
                                    quest.QuestEntity1Id,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, 5)
                                    }
                                )
                            }
                        ),
                        new (
                            "Торговый караван был разграблен разбойниками, причинив Вам значительный ущерб.",
                            5,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, -24)
                                    }
                                ),
                            }
                        ),
                    }
                ),
                new (
                    (int)BaseQuestOptionType.Agressive,
                    "Организовать набег с целью наживы и увеличения могущества.",
                    new QuestOptionResult[]
                    {
                        new (
                            "Набег получился крайне успешным, вы возвращаетесь домой с огромной добычей.",
                            5,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, 120)
                                    }
                                ),
                                new (
                                    EntityType.Organization,
                                    quest.QuestEntity1Id,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, -70)
                                    }
                                )
                            }
                        ),
                        new (
                            "Набег прошёл успешно, добыча явно покроет все затраты на организацию набега.",
                            50,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, 40)
                                    }
                                ),
                                new (
                                    EntityType.Organization,
                                    quest.QuestEntity1Id,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, -20)
                                    }
                                )
                            }
                        ),
                        new (
                            "Набег не увенчался успехом, понеся некоторые потери вы вернулись домой почти с пустыми руками.",
                            40,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, -40)
                                    }
                                )
                            }
                        ),
                        new (
                            "Это была ужасная идея, отряд попал в засаду и понёс ужасные потери.",
                            5,
                            new QuestOptionResultEntity[]
                            {
                                new (
                                    EntityType.Organization,
                                    quest.OrganizationId,
                                    new QuestOptionResultEntityParameter[]
                                    {
                                        new(Domain.EntityParametres.OrganizationPower, -120)
                                    }
                                )
                            }
                        )
                    }
                ),
            };
        }

        public Task<QuestOptionResult> HandleQuestOption(int questOptionId, CancellationToken cancellationToken)
        {
            var options = GetQuestOptions(Quest);
            var option = options.SingleOrDefault(o => o.Id == questOptionId);
            if (option == null)
                throw new ApplicationException("Неверный идентификатор варианта решения квеста.");

            var sumWeight = option.QuestOptionResults.Sum(r => r.Weight);
            var random = new Random().Next(1, sumWeight);

            QuestOptionResult result = null;
            var index = 0;
            while (random > 0)
            {
                result = option.QuestOptionResults[index];
                random -= result.Weight;
                index++;
            }

            return Task.FromResult(result);
        }
    }
}
