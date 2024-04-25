using Eventify;
using Eventify.Saga;
using HandlersAndEvents.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlersAndEvents.Saga
{
    public class SomeEventSaga : ISaga<SomeEvent>
    {
        public Task Succeeded(SomeEvent @event)
        {
            Console.WriteLine("SAGA SUCCESS");
            return Task.CompletedTask;
        }

        public Task Failed(SomeEvent @event, IEventHandler<SomeEvent> eventHandler)
        {
            Console.WriteLine("SAGA ERROR");
            return Task.CompletedTask;
        }
    }
}
