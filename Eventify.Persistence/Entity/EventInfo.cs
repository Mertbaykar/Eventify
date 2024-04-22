using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eventify.Persistence.Entity
{
    [Table("Event", Schema = "Eventify")]
    public class EventInfo
    {
        #region Ctor

        // EF requires
        private EventInfo()
        {

        }

        private EventInfo(EventifyEvent @event)
        {
            this.Id = @event.Id;
            InitData(@event, this);
            this.LastOccurredAt = @event.OccurredAt;
            TryCount++;
        }

        #endregion

        #region Prop & Field

        public Guid Id { get; private set; }
        /// <summary>
        /// Event type assemblyqualifiedname
        /// </summary>
        public string TypeName { get; private set; }
        public string Data { get; private set; }

        public DateTime LastOccurredAt { get; private set; }
        public int TryCount { get; private set; }

        public ICollection<HandleResult> HandleResults { get; private set; }

        #endregion

        public void AddHandleResult(HandleResult handleResult)
        {
            if (HandleResults == null)
                HandleResults = new List<HandleResult>();
            HandleResults.Add(handleResult);
        }

        private void InitData<TEvent>(TEvent @event, EventInfo eventInfo) where TEvent : EventifyEvent
        {
            var inputType = @event.GetType();

            eventInfo.Data = JsonSerializer.Serialize(@event, inputType, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            eventInfo.TypeName = @event.GetType().AssemblyQualifiedName!;
        }

        public static EventInfo Create<TEvent>(TEvent @event) where TEvent : EventifyEvent
        {
            return new EventInfo(@event);
        }
    }
}
