using Eventify.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence.Trigger
{
    public interface IEventTrigger
    {
        //List<EventVM> GetEventsWitHandlers();
        Task<HandleResult> TriggerHandler(Guid handlerId);
    }
}
