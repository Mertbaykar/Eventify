using Eventify.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eventify.Persistence.ViewModel
{
    public class EventVM
    {
        public EventVM()
        {
            HandleResults = new();
        }

        public Guid Id { get; set; }
        /// <summary>
        /// Event type assemblyqualifiedname
        /// </summary>
        public string TypeName { get; set; }
        public string ShortTypeName => GetShortTypeName();
        public string Data { get; set; }
        public DateTime LastOccurredAt { get; set; }

        public int TryCount { get; set; }
        public List<HandleResultVM> HandleResults { get; set; }

        public object GetEventData()
        {
            Type type = Type.GetType(TypeName)!;
            var eventData = JsonSerializer.Deserialize(Data, type)!;

            if (eventData is EventifyEvent) return eventData;
            throw new Exception("Type error while evaluating eventdata");
        }

        private string GetShortTypeName()
        {
            var typePartIndex = TypeName.IndexOf(',');
            var typePart = TypeName.Substring(0, typePartIndex);

            int lastIndex = typePart.LastIndexOf('.');
            string result = typePart.Substring(lastIndex + 1);
            return result;
        }
    }

    public class HandleResultVM
    {

        public Guid Id { get; set; }
        /// <summary>
        /// Handler type assemblyqualifiedname
        /// </summary>
        public string TypeName { get; set; }
        public string ShortTypeName => GetShortTypeName();
        public bool Success { get; set; }
        public int TryCount { get; set; }

        public DateTime LastExecutedAt { get; set; }
        public string? ErrorMessage { get; set; }
        public bool ShowCopyText { get; set; }

        public Guid EventId { get; set; }

        private string GetShortTypeName()
        {
            var typePartIndex = TypeName.IndexOf(',');
            var typePart = TypeName.Substring(0, typePartIndex);

            int lastIndex = typePart.LastIndexOf('.');
            string result = typePart.Substring(lastIndex + 1);
            return result;
        }
    }
}
