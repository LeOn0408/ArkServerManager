using ARKServerManager.ServerService;

namespace ARKServerManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Statistics statistics= new();
            Task task= new(()=>statistics.StartStatistics());
            task.Start();

            CreateHostBuilder(args).Build().Run();

        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:5005").UseStartup<Startup>();
                });
    }
}
