using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Eventify.Persistence.Persistence;
using Eventify.Persistence.Startup.Tasks;

namespace Eventify.Persistence.SqlServer
{
    public static class PersistenceOptionsBuilderExtensions
    {

        public static PersistenceOptionsBuilder UseSqlServer(this PersistenceOptionsBuilder persistenceOptionsBuilder, string connString, bool automigration = true)
        {
            persistenceOptionsBuilder.services.AddDbContextFactory<EventContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(connString, optionsBuilder => optionsBuilder.MigrationsAssembly("Eventify.Persistence.SqlServer"));
            });

            if (automigration)
          persistenceOptionsBuilder.AddStartupTask<MigrationRunner>();


            return persistenceOptionsBuilder;
        }
    }
}
