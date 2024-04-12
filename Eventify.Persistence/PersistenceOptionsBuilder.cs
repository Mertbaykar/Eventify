using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;
using Eventify.Persistence.Startup.Tasks;
using Eventify.Persistence.Startup;

namespace Eventify.Persistence
{
    public class PersistenceOptionsBuilder
    {
        internal IServiceCollection services;

        internal PersistenceOptionsBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        internal PersistenceOptionsBuilder AddStartupTask<TStartupTask>() where TStartupTask : class, IStartupTask
        {
            this.services.AddStartupTask<TStartupTask>();
            return this;
        }
    }
}
