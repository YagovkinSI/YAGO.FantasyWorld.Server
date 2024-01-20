using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YAGO.FantasyWorld.Server.Infrastracture.Database;
using YAGO.FantasyWorld.Server.Infrastracture.Identity;

namespace YAGO.FantasyWorld.Server.Infrastracture
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDatabase(configuration)
                .AddIdentity();

            return services;
        }
    }
}
