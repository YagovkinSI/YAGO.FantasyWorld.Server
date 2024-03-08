using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Common;
using YAGO.FantasyWorld.Server.Domain.HistoryEvents;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.YagoException;

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
            quest.Status = Domain.Enums.QuestStatus.Completed;

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
                case Domain.Enums.EntityType.Organization:
                    await HandleChageOrganization(entity, cancellationToken);
                    break;
                case Domain.Enums.EntityType.Unknown:
                default:
                    throw new ApplicationException("Неизвестный тип данных для изменения");
            }
        }

        private Task HandleChageOrganization(EntityChange entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var organization = Organizations.Find(entity.EntityId);
            foreach (var parameter in entity.QuestOptionResultEntityParameters)
            {
                organization.Power += parameter.EntityParameter switch
                {
                    EntityParametres.OrganizationPower => int.Parse(parameter.Change),
                    _ => throw new ApplicationException("Неизвестный тип параметра организации для изменения"),
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
