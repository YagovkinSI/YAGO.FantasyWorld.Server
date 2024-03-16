using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Entities;
using YAGO.FantasyWorld.Domain.Entities.Enums;
using YAGO.FantasyWorld.Domain.HistoryEvents;
using YAGO.FantasyWorld.Domain.HistoryEvents.Enums;
using YAGO.FantasyWorld.Domain.Quests;
using YAGO.FantasyWorld.Domain.Quests.Enums;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Application.History
{
    public class HistoryService
    {
        private const int QUEST_TYPE_START = 1000000;

        private readonly IHistoryEventDatabaseSerice _historyEventDatabaseSerice;
        private readonly IEntityProviderDatabaseSerice _entityProviderDatabaseSerice;

        public HistoryService(IHistoryEventDatabaseSerice historyEventDatabaseSerice,
            IEntityProviderDatabaseSerice entityProviderDatabaseSerice)
        {
            _historyEventDatabaseSerice = historyEventDatabaseSerice;
            _entityProviderDatabaseSerice = entityProviderDatabaseSerice;
        }

        /// <summary>
        /// Получить три последних события в отношениях двух организаций
        /// </summary>
        /// <param name="organizationFirstId">Идентификатор первой организации</param>
        /// <param name="organizationSecondId">Идентификатор второй организаци</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetOrganizationRelations(long organizationFirstId, long organizationSecondId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = new YagoEntity[]
            {
                new() { Id = organizationFirstId, EntityType = EntityType.Organization },
                new() { Id = organizationSecondId, EntityType = EntityType.Organization },
            };
            var events = await _historyEventDatabaseSerice
                .GetOrganizationHistory(entities, 5);

            return events
                .Select(e => GetEventText(e).GetAwaiter().GetResult());
        }

        private async Task<string> GetEventText(HistoryEvent historyEvent)
        {
            var eventType = (int)historyEvent.Type / 1000000;
            return eventType switch
            { 
                1 => await GetQuestHistoryEventText(historyEvent),
                _ => "Неизвестное событие"
            };
        }

        private async Task<string> GetQuestHistoryEventText(HistoryEvent historyEvent)
        {
            var questType = (int)historyEvent.Type % 1000000 / 100;
            return questType switch
            {
                1 => await GetBaseQuestHistoryEventText(historyEvent), 
                _ => "Неизвестное событие"
            };
        }

        private async Task<string> GetBaseQuestHistoryEventText(HistoryEvent historyEvent)
        {
            var initiatorName = await GetEnityName(historyEvent.HistoryEventEntities.Single(o => o.Role == HitsoryEventEnitiyRole.Initiator));
            var targetName = await GetEnityName(historyEvent.HistoryEventEntities.Single(o => o.Role == HitsoryEventEnitiyRole.Target));

            var historyEventText = new StringBuilder();
            historyEventText.AppendLine($"Владение {initiatorName} обсуждает на совете отношения с владением {targetName}.");

            var optionType = (int)historyEvent.Type % 100 / 10;
            var optionText = optionType switch
            {
                1 => "- Решение: Сохранять нейтралитет и поддерживать мирные отношения.",
                2 => "- Решение: Искать взаимовыгодные сделки и укреплять экономическую связь.",
                3 => "- Решение: Организовать набег с целью наживы и увеличения могущества.",
                _ => "- Неизвестное решение."
            };
            historyEventText.AppendLine(optionText);

            var resultType = (int)historyEvent.Type % 10;
            var resultText = GetQuestResultText(resultType);
            historyEventText.AppendLine(resultText);

            return historyEventText.ToString();
        }

        private static string GetQuestResultText(int resultType)
        {
            return resultType switch
            {
                1 => "- Результат: Большой успех.",
                2 => "- Результат: Успех.",
                3 => "- Результат: Нейтральный результат.",
                4 => "- Результат: Провал.",
                5 => "- Результат: Полный провал.",
                _ => "- Неизвестное результат."
            };
        }

        private async Task<string> GetEnityName(HistoryEventEntity historyEventEntity) => await _entityProviderDatabaseSerice.GetEntityName(historyEventEntity.EntityType, historyEventEntity.EntityId);

        /// <summary>
        /// Создание исторического события на основе результатов квеста
        /// </summary>
        /// <param name="quest">Данные квеста</param>
        /// <param name="optionId">Выбранный идентификатор опции квеста</param>
        /// <param name="result">Данные по результатам квеста</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Историческое событие</returns>
        public Task<HistoryEvent> CreateHistoryEvent(Quest quest, int optionId, QuestOptionResult result, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var type = GetHistoryEventType(quest.Type, optionId, result.Type);
            var enities = GetHistoryEventEntities(quest);
            var weights = CalcEntityWeights(result.EntitiesChange, enities);

            return Task.FromResult(new HistoryEvent
            {
                DateTimeUtc = DateTimeOffset.UtcNow,
                Type = type,
                HistoryEventEntities = enities,
                EntityWeights = weights,
                ParameterChanges = result.EntitiesChange
            });
        }

        private HistoryEventType GetHistoryEventType(QuestType type, int optionId, QuestOptionResultType result)
        {
            var questTypeInt = QUEST_TYPE_START + ((int)type * 100) + (optionId * 10) + ((int)result);
            var questType = (HistoryEventType)questTypeInt;
            return questType == HistoryEventType.Unknown
                ? throw new Domain.Exceptions.YagoException("Не удалось определить тип истрического события")
                : questType;
        }

        private HistoryEventEntityWeight[] CalcEntityWeights(EntityChange[] questOptionResultEntities, HistoryEventEntity[] enities)
        {
            var entityWeights = new List<HistoryEventEntityWeight>();
            foreach (var parameterChanges in questOptionResultEntities)
            {
                var entityWeight = new HistoryEventEntityWeight
                {
                    EntityType = parameterChanges.EntityType,
                    EntityId = parameterChanges.EntityId,
                    Weight = CalcWeight(parameterChanges.EntityParametersChange)
                };
                entityWeights.Add(entityWeight);
            }

            foreach (var entity in enities)
            {
                if (!entityWeights.Exists(w => w.EntityType == entity.EntityType && w.EntityId == entity.EntityId))
                    entityWeights.Add(new HistoryEventEntityWeight { EntityType = entity.EntityType, EntityId = entity.EntityId, Weight = 1 });
            }

            return entityWeights.ToArray();
        }

        private int CalcWeight(EntityParameterChange[] questOptionResultEntityParameters)
        {
            var result = 0;
            foreach (var parameter in questOptionResultEntityParameters)
            {
                result += parameter.EntityParameter switch
                {
                    EntityParameter.OrganizationPower => Math.Abs(int.Parse(parameter.Change)),
                    _ => 0
                };
            }
            return result;
        }

        private HistoryEventEntity[] GetHistoryEventEntities(Quest quest)
        {
            return quest.Type switch
            {
                QuestType.BaseQuest => new HistoryEventEntity[]
                {
                    new(EntityType.Organization, quest.OrganizationId, HitsoryEventEnitiyRole.Initiator),
                    new(EntityType.Organization, quest.QuestEntity1Id, HitsoryEventEnitiyRole.Target)
                },
                _ => throw new Domain.Exceptions.YagoException("Неизвестный тип события для определения ролей")
            };
        }
    }
}
