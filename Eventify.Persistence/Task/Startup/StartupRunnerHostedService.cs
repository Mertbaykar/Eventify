using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Persistence.Task.Startup
{
    internal class StartupRunnerHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public StartupRunnerHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var startupTasks = scope.ServiceProvider.GetServices<IStartupTask>();

                foreach (var task in startupTasks)
                    await task.ExecuteAsync(cancellationToken);
            }
        }

        public System.Threading.Tasks.Task StopAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
