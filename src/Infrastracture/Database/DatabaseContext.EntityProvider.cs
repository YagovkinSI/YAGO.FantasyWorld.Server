using System.Threading.Tasks;
using YAGO.FantasyWorld.Domain.Entities.Enums;
using YAGO.FantasyWorld.Domain.Exceptions;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IEntityProviderDatabaseSerice
    {
        public async Task<string> GetEntityName(EntityType entityType, long entityId)
        {
            switch (entityType)
            {
                case EntityType.Organization:
                    var organization = await Organizations.FindAsync(entityId);
                    return organization.Name;
                default:
                    throw new YagoException("Неизвестный тип сущности");
            }
        }
    }
}
