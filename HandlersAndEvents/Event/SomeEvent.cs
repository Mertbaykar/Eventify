using Eventify;

namespace HandlersAndEvents.Event
{
    public class SomeEvent : EventifyEvent
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
