using ARKServerManager.Database;
using ARKServerManager.Models;
using ARKServerManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ARKServerManager.ServerService
{
    public class ServerProcess
    {
        public static int[] ProcessId { get; set; }
    }
    public class ServerJob:IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<ServerJob> logger;
        
        private Dictionary<int, ServerTask> ActiveTask { get; set; }
        List<ServerTask> Tasks { get; set; }

        private Timer timer;

        public ServerJob(IServiceScopeFactory _scopeFactory, ILogger<ServerJob> _logger)
        {
            scopeFactory = _scopeFactory;
            logger = _logger;
            ActiveTask = new Dictionary<int, ServerTask>();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            string message = $"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Background tasks started";
            //logger.LogInformation(message);
            timer = new(GetJobs, null, 0, 60000);
            
            return Task.CompletedTask;
        }
        public void GetJobs(object state)
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            using var Db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            try
            {
                Statistics statistics = new(scopeFactory);
                statistics.StartStatistics();

                Tasks = Db.Jobs.Where(x => x.Result == 0).AsNoTracking().ToList();
                foreach (ServerTask c in Tasks)
                {
                    bool activeTask = ActiveTask.ContainsKey(c.Id);
                    //вычислить разницу
                    TimeSpan timeToJob = c.DateJob.Subtract(DateTime.Now);
                    if (timeToJob.Hours <= 0 && timeToJob.Minutes <= 1 && !activeTask)
                    {
                        ActiveTask.Add(c.Id, c);
                        Task task = new(() => JobsCreated(c));
                        task.Start();
                    }
                }
                
            }
            catch (Exception ex)
            {

                logger.LogError(ex,"Ошибка");
            }
            
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
            
            //ServerTask edit = Db.Jobs.FirstOrDefault(x => x.Id == c.Id);
            if (c.Repeating)
            {
                int day = (int)DateTime.Now.Subtract(c.DateJob).TotalDays + 1;
                c.DateJob = c.DateJob.AddDays(day);
            }
            Db.Jobs.Update(c);
            Db.SaveChanges();
            ActiveTask.Remove(c.Id);
            try
            {
                switch (c.Type)
                {
                    case 1:
                        Backup(Db, c);
                        break;
                    case 2:
                        //TODO: Доработать
                        //Update(Db, edit);
                        break;
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка");
            }
            

        }
       
        private static void Backup(DatabaseContext Db, ServerTask edit)
        {
            //logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Backup started");
            BackupSave backupSave = new();
            backupSave.Backup(Db, edit.ServerId);
            //logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Backup completed");
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            return Task.CompletedTask;
        }
    }
}
