using ARKServerManager.Database;
using ARKServerManager.Models;
using ARKServerManager.Servers.Ark;
using ARKServerManager.ServerService;
using Microsoft.EntityFrameworkCore;

namespace ARKServerManager.Servers
{
    public class JobCreator
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ServerJob> _logger;

        private Dictionary<int, ServerTask> ActiveTask { get; set; }
        List<ServerTask> Tasks { get; set; }

        private Timer timer;

        public JobCreator(IServiceScopeFactory scopeFactory, ILogger<ServerJob> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            ActiveTask = new Dictionary<int, ServerTask>();
        }

        public void CreateJob()
        {
            timer = new(GetJobs, null, 0, 60000);
        }

        public void GetJobs(object state)
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            using var Db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            try
            {
                CreateWorkWithoutDates();


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

                _logger.LogError(ex, "Ошибка");
            }

        }
        private void CreateWorkWithoutDates()
        {
            CreateStatisticJob();
            CreateArkJob();
        }

        private void CreateArkJob()
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            Server server = db.Server.FirstOrDefault(s => s.Id == (int)GameServer.Ark);
            if(server is not null)
            {
                new ArkLogs(server,db).ReadFileLog();
            }
        }

        private void CreateStatisticJob()
        {
            Statistics statistics = new(_scopeFactory);
            statistics.StartStatistics();
        }
        private void JobsCreated(ServerTask c)
        {
            using IServiceScope scope = _scopeFactory.CreateScope();
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
                _logger.LogError(ex, "Ошибка");
            }


        }

        private static void Backup(DatabaseContext Db, ServerTask edit)
        {
            var server = Db.Server.FirstOrDefault(s => s.Id == edit.ServerId);
            if (server is null)
                return;
            Backup backupSave = new(server);
            backupSave.LaunchBackup();
        }
    }
}
