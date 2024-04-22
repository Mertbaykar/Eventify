using Eventify.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence.Persistence
{
    internal interface IEventPersistence
    {
        System.Threading.Tasks.Task Persist(EventInfo eventInfo);
    }
}
