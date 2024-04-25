using Eventify.Persistence.Entity;
using Eventify.Persistence.Persistence;
using Eventify.Publish;
using Eventify.Saga;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence
{
    internal class PersistentPublisher : PublisherBase, IPublisher
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IEventPersistence eventPersistence;

        public PersistentPublisher(IServiceScopeFactory serviceScopeFactory, IEventPersistence eventPersistence)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.eventPersistence = eventPersistence;
        }

        public async Task<bool> Publish<TEvent>(TEvent @event) where TEvent : EventifyEvent
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var handlerInstances = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
                handlerInstances = OrderHandlers(handlerInstances);

                var saga = scope.ServiceProvider.GetService<ISaga<TEvent>>();
                IEventHandler<TEvent>? failedHandler = null;

                EventInfo eventInfo = EventInfo.Create(@event);

                foreach (var handlerInstance in handlerInstances)
                {
                    HandleResult handleResult = HandleResult.Create(handlerInstance, @event);

                    try
                    {
                        if (failedHandler == null)
                        {
                            await handlerInstance.Handle(@event);
                            handleResult.MarkSuccess();
                        }
                    }
                    catch (Exception ex)
                    {
                        handleResult.MarkFail(ex.Message);
                        failedHandler = handlerInstance;
                    }

                    eventInfo.AddHandleResult(handleResult);
                }

                await eventPersistence.Persist(eventInfo);

                // callback
                if (saga != null)
                {
                    if (failedHandler == null) await saga.Succeeded(@event);
                    else await saga.Failed(@event, failedHandler);
                }

                return failedHandler == null;
            }
        }
    }
}
