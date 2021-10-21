using ArkWeb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz.Impl;
using Quartz;
using ArkWeb.Models.Server;

namespace web_ark_v2
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //string osver = Environment.OSVersion.VersionString;
            //osver = osver.Substring(0, 4);
            //if (osver == "Unix")
            //    ServerSettings.ServerPath = "/mnt/data/Projects/ArkServer/arkv3/";
            //else
            //    ServerSettings.ServerPath = "P:\\ArkServer\\arkv3\\";
            //Jobs jobs = new();
            //Task task = new(() => jobs.GetJobs());
            //task.Start();
            CreateHostBuilder(args).Build().Run();
            
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
