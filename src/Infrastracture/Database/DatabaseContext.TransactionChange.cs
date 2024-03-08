using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Entities;
using YAGO.FantasyWorld.Domain.Entities.Enums;
using YAGO.FantasyWorld.Domain.Exceptions;
using YAGO.FantasyWorld.Domain.HistoryEvents;
using YAGO.FantasyWorld.Domain.Quests.Enums;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IDatabaseTransactionChangeService
    {
        public async Task HandleTransactionChange(
            HistoryEvent historyEvent,
            long questId,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var quest = Quests.Find(questId);
            quest.Status = QuestStatus.Completed;

            foreach (var entity in historyEvent.ParameterChanges)
            {
                await HandleChageEntity(entity, cancellationToken);
            }

            await CreateHistoryEvent(historyEvent, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            await SaveChangesAsync(cancellationToken);
        }

        private async Task HandleChageEntity(
            EntityChange entity,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            switch (entity.EntityType)
            {
                case EntityType.Organization:
                    await HandleChageOrganization(entity, cancellationToken);
                    break;
                case EntityType.Unknown:
                default:
                    throw new YagoException("Неизвестный тип данных для изменения");
            }
        }

        private Task HandleChageOrganization(EntityChange entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var organization = Organizations.Find(entity.EntityId);
            foreach (var parameter in entity.EntityParametersChange)
            {
                organization.Power += parameter.EntityParameter switch
                {
                    EntityParameter.OrganizationPower => int.Parse(parameter.Change),
                    _ => throw new YagoException("Неизвестный тип параметра организации для изменения"),
                };
            }
            return Task.CompletedTask;
        }

        private Task CreateHistoryEvent(HistoryEvent historyEvent, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var historyEventDatabase = new Models.HistoryEvent
            {
                DateTimeUtc = historyEvent.DateTimeUtc,
                Type = historyEvent.Type,
                HistoryEventEntities = JsonConvert.SerializeObject(historyEvent.HistoryEventEntities),
                ParameterChanges = JsonConvert.SerializeObject(historyEvent.ParameterChanges)
            };
            HistoryEvents.Add(historyEventDatabase);

            foreach (var entityWeight in historyEvent.EntityWeights)
            {
                var entityWeightDatabase = new Models.HistoryEventEntityWeight
                {
                    HistoryEvent = historyEventDatabase,
                    EntityType = entityWeight.EntityType,
                    EntityId = entityWeight.EntityId,
                    Weight = entityWeight.Weight
                };
                HistoryEventEntityWeights.Add(entityWeightDatabase);
            }

            return Task.CompletedTask;
        }
    }
}
