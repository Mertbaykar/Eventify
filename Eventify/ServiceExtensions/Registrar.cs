using Eventify.Publish;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Eventify.ServiceExtensions.TypeExtensions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Eventify.Persistence")]
namespace Eventify.ServiceExtensions
{
    public static class Registrar
    {
        public static IServiceCollection AddEventify(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            return services.AddEventifyCore<Publisher>(assemblies);
        }

        internal static IServiceCollection AddEventifyCore<TPublisher>(this IServiceCollection services, IEnumerable<Assembly> assemblies) where TPublisher : class, IPublisher
        {
            return services
                .RegisterPublisher<TPublisher>()
                .RegisterHandlers(assemblies);
        }

        private static IServiceCollection RegisterHandlers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var handlerTypes = FindEventHandlerTypes(assemblies);

            foreach (var handlerType in handlerTypes)
            {
                var handlerInterface = FindHandlerInterface(handlerType)!;
                services.AddTransient(handlerInterface, handlerType);
            }

            return services;
        }

        private static IServiceCollection RegisterPublisher<TPublisher>(this IServiceCollection services) where TPublisher : class, IPublisher
        {
            return services.AddSingleton<IPublisher, TPublisher>();
        }

        private static Type[] FindEventHandlerTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                   .SelectMany(assembly => assembly.DefinedTypes)
                   .Where(ImplementsHandler)
                   .ToArray();
        }

    }
}
