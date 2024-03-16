using System;
using System.Collections.Generic;
using System.Linq;
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
                .GetOrganizationHistory(entities, 3);

            return events
                .Select(e => GetEventText(e).GetAwaiter().GetResult());
        }

        private async Task<string> GetEventText(HistoryEvent historyEvent)
        {
            var initiatorName = await GetEnityName(historyEvent.HistoryEventEntities.Single(o => o.Role == HitsoryEventEnitiyRole.Initiator));
            var targetName = await GetEnityName(historyEvent.HistoryEventEntities.Single(o => o.Role == HitsoryEventEnitiyRole.Target));

            return historyEvent.Type switch
            {
                HistoryEventType.BaseQuest_Neitral_CriticalSuccess =>
                    $"Владение {initiatorName} сохраняет нейтралитет с владением {targetName}.",
                HistoryEventType.BaseQuest_Neitral_Success =>
                    $"Владение {initiatorName} сохраняет нейтралитет с владением {targetName}.",
                HistoryEventType.BaseQuest_Neitral_Neitral =>
                    $"Владение {initiatorName} сохраняет нейтралитет с владением {targetName}.",
                HistoryEventType.BaseQuest_Neitral_Fail =>
                    $"Владение {initiatorName} сохраняет нейтралитет с владением {targetName}.",
                HistoryEventType.BaseQuest_Neitral_CriticalFail =>
                    $"Владение {initiatorName} сохраняет нейтралитет с владением {targetName}.",
                HistoryEventType.BaseQuest_Friendly_CriticalSuccess =>
                    $"Владение {initiatorName} организует крайне успешную торговую сделку с владением {targetName}.",
                HistoryEventType.BaseQuest_Friendly_Success =>
                    $"Владение {initiatorName} организует успешную торговую сделку с владением {targetName}.",
                HistoryEventType.BaseQuest_Friendly_Neitral =>
                    $"Владение {initiatorName} безуспешно пытается организовать торговую сделку с владением {targetName}.",
                HistoryEventType.BaseQuest_Friendly_Fail =>
                    $"Владение {initiatorName} организует невыгодню для себя торговую сделку с владением {targetName}.",
                HistoryEventType.BaseQuest_Friendly_CriticalFail =>
                $"Владение {initiatorName} организует торговую сделку с владением {targetName}, но торговый караван разграблен по пути разбойниками.",
                HistoryEventType.BaseQuest_Agressive_CriticalSuccess =>
                    $"Владение {initiatorName} совершает набег на владение {targetName} и полностью разоряет область.",
                HistoryEventType.BaseQuest_Agressive_Success =>
                    $"Владение {initiatorName} совершает успешный набег на владение {targetName}.",
                HistoryEventType.BaseQuest_Agressive_Neitral =>
                    $"Неизвестное событие {HistoryEventType.BaseQuest_Agressive_Neitral}",
                HistoryEventType.BaseQuest_Agressive_Fail =>
                    $"Владение {initiatorName} совершает набег на владение {targetName}, но понеся некоторые потери вы вернулись домой почти с пустыми руками.",
                HistoryEventType.BaseQuest_Agressive_CriticalFail =>
                    $"Владение {initiatorName} совершает набег на владение {targetName}, но отряд попал в засаду и понёс ужасные потери.",
                _ => "Неизвестное событие",
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
