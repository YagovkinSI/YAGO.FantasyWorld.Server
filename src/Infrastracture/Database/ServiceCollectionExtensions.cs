using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YAGO.FantasyWorld.Server.Application.Interfaces;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public static partial class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabase(
                this IServiceCollection services,
                IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                ));

            services
                .AddScoped<IUserDatabaseService, DatabaseContext>()
                .AddScoped<IOrganizationDatabaseService, DatabaseContext>()
                .AddScoped<IFillDatabaseService, DatabaseContext>()
                .AddScoped<IQuestDatabaseService, DatabaseContext>();

            return services;
        }
    }
}
