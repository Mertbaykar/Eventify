using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence
{
    public class OptionsBuilder
    {
        internal IServiceCollection services;

        internal OptionsBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public OptionsBuilder UseEntityFrameworkPersistence(Action<PersistenceOptionsBuilder> persistenceOptionsBuilder)
        {
            var optionsBuilder = new PersistenceOptionsBuilder(services);
            persistenceOptionsBuilder.Invoke(optionsBuilder);
            return this;
        }
    }
}
