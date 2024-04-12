namespace Eventify
{
    public class EventifyEvent
    {
        private Guid id = Guid.NewGuid();
        public Guid Id => id;

        private DateTime occurredAt = DateTime.Now;
		public DateTime OccurredAt => occurredAt;
     
    }
}
