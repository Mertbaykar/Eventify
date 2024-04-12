using Eventify.Publish;
using Eventify.ServiceExtensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Eventify.ServiceExtensions.TypeExtensions;
using static Eventify.ServiceExtensions.Registrar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Eventify.Persistence.Persistence;
using System.Runtime.CompilerServices;
using Eventify.Persistence.Startup;
using Eventify.Persistence.EventOperator;

[assembly: InternalsVisibleTo("Eventify.Persistence.SqlServer")]
namespace Eventify.Persistence
{
    public static class PersistentExtensions
    {

        public static IServiceCollection AddEventify(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<OptionsBuilder> optionsAction)
        {
            return services.AddEventifyPersistence(assemblies, optionsAction);
        }

        private static IServiceCollection AddEventifyPersistence(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<OptionsBuilder> config)
        {
            services.AddEventifyCore<PersistentPublisher>(assemblies)
                .RegisterEventPersistence()
                .RegisterDashboardServices()
                .AddStartupRunner();

            var builder = new OptionsBuilder(services);
            config.Invoke(builder);
            return services;
        }

        private static IServiceCollection AddStartupRunner(this IServiceCollection services)
        {
            return services.AddHostedService<StartupRunnerHostedService>();
        }

        private static IServiceCollection RegisterEventPersistence(this IServiceCollection services) 
        {
            return services.AddSingleton<IEventPersistence, EventPersistence>();
        }

        private static IServiceCollection RegisterDashboardServices(this IServiceCollection services)
        {
            return services.AddSingleton<IEventOperator, EventOperator.EventOperator>();
        }

        internal static IServiceCollection AddStartupTask<TStartupTask>(this IServiceCollection services) where TStartupTask : class, IStartupTask
        {
            return services
                .AddScoped<TStartupTask>()
                .AddScoped<IStartupTask, TStartupTask>((IServiceProvider sp) => sp.GetRequiredService<TStartupTask>());
        }
    }
}
