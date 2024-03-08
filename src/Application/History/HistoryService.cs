﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.ApiContracts.Common.Enums;
using YAGO.FantasyWorld.ApiContracts.Common.Models;
using YAGO.FantasyWorld.Domain;
using YAGO.FantasyWorld.ApiContracts.QuestApi.Enums;
using YAGO.FantasyWorld.ApiContracts.QuestApi.Models;
using YAGO.FantasyWorld.Domain;
using YAGO.FantasyWorld.Domain.Enums;

namespace YAGO.FantasyWorld.Server.Application.History
{
    public class HistoryService
    {
        private const int QUEST_TYPE_START = 1000000;

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
                if (!entityWeights.Any(w => w.EntityType == entity.EntityType && w.EntityId == entity.EntityId))
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
