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
        private List<int> process=new();
        public IConfiguration AppConfiguration { get; private set; }
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<ServerJob> logger;
        
        private Dictionary<int, ServerTask> ActiveTask { get; set; }
        List<ServerTask> Tasks { get; set; }

        public ServerJob(IServiceScopeFactory _scopeFactory, ILogger<ServerJob> _logger)
        {
            scopeFactory = _scopeFactory;
            logger = _logger;
            
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            string message = $"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Background tasks started";
            logger.LogInformation(message);
            Timer timer = new(GetJobs, null, 0, 60000);
            ActiveTask = new Dictionary<int, ServerTask>();
            return Task.CompletedTask;
        }
        public void GetJobs(object state)
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            using var Db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            
            try
            {
                
                Tasks = Db.Jobs.Where(x => x.Result == 0).AsNoTracking().ToList();
                foreach (ServerTask c in Tasks)
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
            
            ServerTask edit = Db.Jobs.FirstOrDefault(x => x.Id == c.Id);
            if (c.Repeating)
            {
                int day = (int)DateTime.Now.Subtract(c.DateJob).TotalDays;
                edit.DateJob = c.DateJob.AddDays(day);
            }
            Db.SaveChanges();
            
            try
            {
                switch (c.Type)
                {
                    case 1: 
                        Backup(Db, edit);
                        break;
                    case 2:
                        //TODO: Доработать
                        //Update(Db, edit);
                        break;
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} {ex.Message}");
            }
            ActiveTask.Remove(c.Id);

        }
        private async void Update(DatabaseContext Db, ServerTask edit)
        {
            logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Update started");
            Server server = Db.Server.FirstOrDefault(s => s.Id == edit.ServerId);
            ArkServer ark = new();

            await ark.Save(server);
            StopChildrenProcess();//ркон не всегда корректно срабатывает. Принудительно закрываем все ранее запущенные приложения, хотя лучше использовать коммнду quit
            
            await ark.UpdateAsync(logger);
            process.Add(ark.Launch(server));
            logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Update completed");
            
        }
        private void Backup(DatabaseContext Db, ServerTask edit)
        {
            logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Backup started");
            BackupSave backupSave = new();
            backupSave.Backup(Db, edit.ServerId);
            logger.LogInformation($"{DateTime.Now:yyyy.MM.dd HH:mm:ss} Backup completed");
            
        }

        void StopChildrenProcess()
        {
            foreach (var item in process)
            {
                var proc = Process.GetProcessById(item);
                proc.Kill(true);
                process.Remove(item);
            }

            //TODO: Добавить очистку всех серверов, которые не контролируются нами. Или выдать предупреждение что обнаружены не закрытые сервера
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            StopChildrenProcess();
            return Task.CompletedTask;
        }
    }
}
