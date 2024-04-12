using Eventify.Persistence.Entity;
using Eventify.Persistence.Persistence;
using Eventify.Publish;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence
{
    internal class PersistentPublisher : IPublisher
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IEventPersistence eventPersistence;

        public PersistentPublisher(IServiceScopeFactory serviceScopeFactory, IEventPersistence eventPersistence)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.eventPersistence = eventPersistence;
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : EventifyEvent
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var handlerInstances = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

                EventInfo eventInfo = EventInfo.Create(@event);

                foreach (var handlerInstance in handlerInstances)
                {
                    HandleResult handleResult = HandleResult.Create(handlerInstance, @event);

                    try
                    {
                        await handlerInstance.Handle(@event);
                        handleResult.MarkSuccess();
                    }
                    catch (Exception ex)
                    {
                        handleResult.MarkFail(ex.Message);
                    }
                    finally
                    {
                        eventInfo.AddHandleResult(handleResult);
                    }
                }

                await eventPersistence.Persist(eventInfo);

            }
        }
    }
}
