using Eventify.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence.Startup.Tasks
{
    internal class MigrationRunner : IStartupTask
    {
        private readonly IDbContextFactory<EventContext> eventContextFactory;

        internal MigrationRunner(IDbContextFactory<EventContext> dbContextFactoryFactory)
        {
            eventContextFactory = dbContextFactoryFactory;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using EventContext dbContext = await eventContextFactory.CreateDbContextAsync();
            await dbContext.Database.MigrateAsync(cancellationToken);
            await dbContext.DisposeAsync();
        }
    }
}
