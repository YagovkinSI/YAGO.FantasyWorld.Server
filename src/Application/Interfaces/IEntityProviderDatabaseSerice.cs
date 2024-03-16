using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Entities.Enums;

namespace YAGO.FantasyWorld.Server.Application.Interfaces
{
    public interface IEntityProviderDatabaseSerice
    {
        Task<string> GetEntityName(EntityType entityType, long entityId);
    }
}
