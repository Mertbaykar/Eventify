using Eventify;
using HandlersAndEvents.Event;

namespace HandlersAndEvents.Handler
{
    public abstract class HandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : EventifyEvent
    {
        public abstract Task Handle(TEvent @event);
    }
}
