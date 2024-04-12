using Eventify.Persistence.Entity;
using Eventify.Persistence.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence.EventOperator
{
    public interface IEventOperator
    {
        Task<List<EventVM>> GetEventsWitHandlers();
        Task<HandleResult> TriggerHandler(Guid handlerId);
    }
}
