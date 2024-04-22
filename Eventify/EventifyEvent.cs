using System.Text.Json.Serialization;

namespace Eventify
{
    public class EventifyEvent
    {
        private Guid id = Guid.NewGuid();
        [JsonIgnore]
        public Guid Id => id;

        private DateTime occurredAt = DateTime.Now;
        [JsonIgnore]
        public DateTime OccurredAt => occurredAt;
     
    }
}
