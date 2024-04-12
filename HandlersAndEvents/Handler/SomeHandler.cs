using Eventify;
using HandlersAndEvents.Event;

namespace HandlersAndEvents.Handler
{
    public class SomeHandler : IEventHandler<SomeEvent>
    {
        public Task Handle(SomeEvent @event)
        {
            Console.WriteLine($"Event tetiklendi: {@event.Name} {@event.Lastname}");
            return Task.CompletedTask;
        }
    }
}
