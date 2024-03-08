using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using YAGO.FantasyWorld.Server.Application.Admin;
using YAGO.FantasyWorld.Server.Application.Authorization;
using YAGO.FantasyWorld.Server.Application.History;
using YAGO.FantasyWorld.Server.Application.Organizations;
using YAGO.FantasyWorld.Server.Application.Quests;
using YAGO.FantasyWorld.Server.Application.UserLastActivity;
using YAGO.FantasyWorld.Server.Application.Users;
using YAGO.FantasyWorld.Server.Host.Middlewares;
using YAGO.FantasyWorld.Server.Infrastracture;

namespace YAGO.FantasyWorld.Server.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);

            AddAppServices(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "YAGO.FantasyWorld.Server.Host", Version = "v1" });
            });
        }

        private static void AddAppServices(IServiceCollection services)
        {
            services.AddScoped<AdminService>();
            AddUserServices(services);
            services.AddScoped<OrganizationService>();
            AddQuestServices(services);
            services.AddScoped<HistoryService>();
        }

        private static void AddUserServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<AuthorizationService>();
            services.AddScoped<UserLastActivityService>();
        }

        private static void AddQuestServices(IServiceCollection services)
        {
            services.AddScoped<QuestService>();
            services.AddScoped<QuestGenerator>();
            services.AddScoped<QuestReadinessService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YAGO.FantasyWorld.Server.Host v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
