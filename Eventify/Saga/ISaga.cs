using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Saga
{
    public interface ISaga<TEvent> where TEvent : EventifyEvent
    {
        /// <summary>
        /// Callback to run when the event is succesfully handled by all handlers
        /// </summary>
        /// <param name="event">The event</param>
        /// <returns></returns>
        Task Succeeded(TEvent @event);

        /// <summary>
        /// Callback to run when event has failed by any eventhandler.
        /// </summary>
        /// <param name="event">The event</param>
        /// <param name="eventHandler">Handler that failed</param>
        /// <returns></returns>
        Task Failed(TEvent @event, IEventHandler<TEvent> eventHandler);
    }
}
