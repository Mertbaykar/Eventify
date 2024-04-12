using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify
{
    public interface IEventHandler<TEvent> where TEvent : EventifyEvent
    {
       Task Handle(TEvent @event);
    }
}
