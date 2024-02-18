using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Domain;
using YAGO.FantasyWorld.Server.Domain.Quests;
using ApplicationException = YAGO.FantasyWorld.Server.Domain.Exceptions.ApplicationException;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IDatabaseTransactionChangeService
    {
        public async Task HandleTransactionChange(
            QuestOptionResultEntity[] questOptionResultEntities,
            long questId,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var quest = Quests.Find(questId);
            quest.Status = Domain.Enums.QuestStatus.Completed;

            cancellationToken.ThrowIfCancellationRequested();
            foreach (var entity in questOptionResultEntities)
            {
                await HandleChageEntity(entity, cancellationToken);
            }

            cancellationToken.ThrowIfCancellationRequested();
            await SaveChangesAsync(cancellationToken);
        }

        private async Task HandleChageEntity(
            QuestOptionResultEntity entity,
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

        private Task HandleChageOrganization(QuestOptionResultEntity entity, CancellationToken cancellationToken)
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
    }
}
