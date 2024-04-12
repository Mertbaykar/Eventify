using System.ComponentModel.DataAnnotations.Schema;

namespace Eventify.Persistence.Entity
{
    [Table("HandleResult", Schema = "Eventify")]
    public class HandleResult
    {
        private HandleResult()
        {

        }

        #region Prop & Field

        public Guid Id { get; private set; }
        /// <summary>
        /// Handler tipinin assembly dahil tam adı
        /// </summary>
        public string TypeName { get; private set; }
        public bool Success { get; private set; }
        public int TryCount { get; private set; }
        public DateTime? LastExecutedAt { get; private set; }

        public string? ErrorMessage { get; private set; }

        public Guid EventId { get; private set; }
        [ForeignKey(nameof(EventId))]
        public EventInfo Event { get; private set; }

        #endregion

        public static HandleResult Create<TEventHandler, TEvent>(TEventHandler eventHandler, TEvent @event) where TEventHandler : IEventHandler<TEvent> where TEvent : EventifyEvent
        {
            var handlerResult = new HandleResult();
            handlerResult.Id = Guid.NewGuid();
            handlerResult.TypeName = eventHandler.GetType().AssemblyQualifiedName!;
            handlerResult.EventId = @event.Id;
            return handlerResult;
        }

        public void MarkSuccess()
        {
            Success = true;
            LastExecutedAt = DateTime.Now;
            TryCount++;
            ErrorMessage = null;
        }

        public void MarkFail(string errorMessage)
        {
            Success = false;
            LastExecutedAt = DateTime.Now;
            TryCount++;
            ErrorMessage = errorMessage;
        }
    }
}
