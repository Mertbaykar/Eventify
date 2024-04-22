using Eventify.Persistence.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence.Persistence
{
    internal class EventPersistence : IEventPersistence
    {
        private readonly IDbContextFactory<EventContext> eventContextFactory;

        public EventPersistence(IDbContextFactory<EventContext> eventContextFactory)
        {
            this.eventContextFactory = eventContextFactory;
        }

        public async System.Threading.Tasks.Task Persist(EventInfo eventInfo)
        {
            using (var eventContext = await eventContextFactory.CreateDbContextAsync())
            {
                try
                {
                    await eventContext.Event.AddAsync(eventInfo);
                    await eventContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
