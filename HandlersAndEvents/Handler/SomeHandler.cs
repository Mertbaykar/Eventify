﻿using Eventify;
using Eventify.Attribute;
using HandlersAndEvents.Event;

namespace HandlersAndEvents.Handler
{
    [ExecuteOrder(1)]
    public class SomeHandler : IEventHandler<SomeEvent>
    {
        public Task Handle(SomeEvent @event)
        {
            Console.WriteLine($"Event tetiklendi: {@event.Name} {@event.Lastname}");
            return Task.CompletedTask;
        }
    }
}
