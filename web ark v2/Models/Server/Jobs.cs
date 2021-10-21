using ArkWeb.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArkWeb.Models.Server
{
    public class Jobs : Controller
    {
        public IConfiguration AppConfiguration { get; private set; }
        private ApplicationContext Db { get; set; }

        List<ArkJobs> jobs = null;

        public Jobs()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            AppConfiguration = builder.Build();
            IConfigurationSection connStrings = AppConfiguration.GetSection("ConnectionStrings");
            string defaultConnection = connStrings.GetSection("DefaultConnection").Value;
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            DbContextOptions<ApplicationContext> options = optionsBuilder
                    .UseSqlServer(defaultConnection)
                    .Options;
            Db = new ApplicationContext(options);
        }
    
        

        public void GetJobs()
        {
            while (true)
            {
                
                // получать те у которых Result = 0
                jobs = Db.Ark_Jobs.AsNoTracking().Where(x => x.Result == 0).ToList();


                foreach (ArkJobs c in jobs)
                {
                    //вычислить разницу
                    TimeSpan timeToJob = c.DateJob.Subtract(DateTime.Now);
                    if (timeToJob.Hours<=0 && timeToJob.Minutes <= 10)
                    {
                        Task task = new(() => JobsCreated(c));
                        task.Start();
                    }
                        

                    //если разница меньше 10 минут создать таск


                }


                TimeSpan time = new(0, 10, 0);
                Thread.Sleep(time);
            }
        }
        void JobsCreated(ArkJobs c)
        {
            //id, type, data
            TimeSpan timeToJob = c.DateJob.Subtract(DateTime.Now);
            Thread.Sleep(timeToJob);
            ArkJobs edit = Db.Ark_Jobs.FirstOrDefault(x => x.Id == c.Id);
            edit.DateJob = c.DateJob.AddDays(1);
            Db.SaveChanges();
            BackupSave backupSave = new();
            backupSave.Backup();
        }
        
    }
}
