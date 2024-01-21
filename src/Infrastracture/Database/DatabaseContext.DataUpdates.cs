using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YAGO.FantasyWorld.Server.Application.Interfaces;
using YAGO.FantasyWorld.Server.Infrastracture.Database.Models;
using YAGO.FantasyWorld.Server.Infrastracture.Database.Updates;

namespace YAGO.FantasyWorld.Server.Infrastracture.Database
{
    public partial class DatabaseContext : IFillDatabaseService
    {
        private readonly List<Func<IUpdateDataTask>> _updates = new()
        {
            { () => new UpdateDataToVersion1Task() }
        };

        public Task FillDatabase(CancellationToken cancellationToken)
        {
            var currentDataVersion = GetCurrentVersion();
            var maxVersion = _updates.Count;

            for (var i = currentDataVersion; i < maxVersion; i++)
            {
                var task = _updates[i].Invoke();
                task.Execute(this);

                var newVersion = i + 1;
                AddVersionUpdate(newVersion);
            }

            return Task.CompletedTask;
        }

        private int GetCurrentVersion()
        {
            return !DataUpdates.Any()
                ? 0
                : DataUpdates
                    .Max(v => v.Version);
        }

        private void AddVersionUpdate(int versionData)
        {
            var versionUpdate = new DataUpdate
            {
                Version = versionData,
                UpdateDateTime = DateTimeOffset.Now
            };

            DataUpdates.Add(versionUpdate);
            SaveChanges();
        }
    }
}
