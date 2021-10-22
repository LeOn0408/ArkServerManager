using ARKServerManager.Database;
using ARKServerManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARKServerManager.ServerService
{
    public class Jobs:IHostedService
    {
        public IConfiguration AppConfiguration { get; private set; }
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<Jobs> logger;
        Timer timer;
        private Dictionary<int, ServerTask> ActiveTask { get; set; }
        List<ServerTask> jobs { get; set; }

        public Jobs(IServiceScopeFactory _scopeFactory, ILogger<Jobs> _logger)
        {
            scopeFactory = _scopeFactory;
            logger = _logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Background tasks started");
            ActiveTask = new Dictionary<int, ServerTask>();
            timer = new(GetJobs,null,0,60000);
            return Task.CompletedTask;
        }
        public void GetJobs(object state)
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            using var Db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            
            try
            {
                
                jobs = Db.Jobs.Where(x => x.Result == 0).AsNoTracking().ToList();
                foreach (ServerTask c in jobs)
                {
                    bool activeTask = ActiveTask.ContainsKey(c.Id);
                    //вычислить разницу
                    TimeSpan timeToJob = c.DateJob.Subtract(DateTime.Now);
                    if (timeToJob.Hours <= 0 && timeToJob.Minutes <= 5 && !activeTask)
                    {
                        ActiveTask.Add(c.Id, c);
                        Task task = new(() => JobsCreated(c));
                        task.Start();
                    }
                }
            }
            catch (Exception)
            {

            }
            Statistics statistics = new(scopeFactory);
            statistics.StartStatistics();
        }
        void JobsCreated(ServerTask c)
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            using var Db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            //id, type, data
            TimeSpan timeToJob = c.DateJob.Subtract(DateTime.Now);
            if (timeToJob.TotalMilliseconds > 0)
            {
                Thread.Sleep(timeToJob);
            }
            logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Backup started");
            ServerTask edit = Db.Jobs.FirstOrDefault(x => x.Id == c.Id);
            edit.DateJob = c.DateJob.AddDays(1);
            Db.SaveChanges();
            ActiveTask.Remove(c.Id);
            try
            {
                BackupSave backupSave = new();
                backupSave.Backup(Db, edit.ServerId);
                logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Backup completed");
            }
            catch(Exception ex)
            {
                logger.LogError($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} {ex.Message}");
            }
            
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
