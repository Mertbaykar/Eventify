using Eventify.Persistence.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence.Persistence
{
    internal class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        internal DbSet<EventInfo> Event { get; set; }
        internal DbSet<HandleResult> HandleResult { get; set; }
    }
}
