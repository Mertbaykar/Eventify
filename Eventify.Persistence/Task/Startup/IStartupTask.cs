using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence.Task.Startup
{

    internal interface IStartupTask
    {
        System.Threading.Tasks.Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
