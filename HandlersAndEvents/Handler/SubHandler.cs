using Eventify;
using HandlersAndEvents.Event;

namespace HandlersAndEvents.Handler
{
    public class SubHandler : HandlerBase<SomeEvent>
    {
        public override Task Handle(SomeEvent @event)
        {
            Console.WriteLine($"Esas class: {GetType().Name}, base class: {typeof(HandlerBase<SomeEvent>).Name}");
            return Task.CompletedTask;
        }
    }
}
