using ARKServerManager.Database;
using ARKServerManager.Models;
using ARKServerManager.Servers;
using Microsoft.EntityFrameworkCore;

namespace ARKServerManager.ServerService
{
    public class ServerJob:IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ServerJob> _logger;

        public ServerJob(IServiceScopeFactory scopeFactory, ILogger<ServerJob> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            JobCreator job = new(_scopeFactory, _logger);
            job.CreateJob();
            return Task.CompletedTask;
        }
       

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
