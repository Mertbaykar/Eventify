using Eventify;
using Eventify.Attribute;
using HandlersAndEvents.Event;

namespace HandlersAndEvents.Handler
{
    [ExecuteOrder(2)]
    public class SomeHandler2 : IEventHandler<SomeEvent>
    {
        public Task Handle(SomeEvent @event)
        {
            Console.WriteLine($"Event tetiklendi 2: {@event.Name} {@event.Lastname}");
            return Task.CompletedTask;
        }
    }
}
