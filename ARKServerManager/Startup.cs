
using Microsoft.Data.Sqlite;
using ARKServerManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Hosting;
using ARKServerManager.ServerService;

namespace ARKServerManager
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddLogging(
            builder =>
            {
                builder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("NToastNotify", LogLevel.Warning)
                       .AddConsole();
            });

            string TypeDB = "MySQL";//вывести в setting
            if (TypeDB == "MySQL")
            {
                string connection = Configuration.GetConnectionString("MySQLConnection");
                ServerVersion vesrion = ServerVersion.AutoDetect(connection);
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseMySql(connection, vesrion,
                    mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 1,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                    }));

            }
            else
            {
                IConfigurationSection connStrings = Configuration.GetSection("ConnectionStrings");
                string connectionString = connStrings.GetSection("SQLiteConnection").Value;
                var connection = new SqliteConnection(connectionString);
                services.AddDbContext<DatabaseContext>(options =>
                   options.UseSqlite(connection));
            }
            services.AddCors();
            services.AddHostedService<ServerJob>();
            //Console.WriteLine(Configuration.GetConnectionString("MySQLConnection"));
        }
    
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext db)
        {
            db.Database.EnsureCreated();
            db.Database.Migrate();

            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }
            //app.UseCors(builder => builder.WithOrigins("https://localhost:7131", "https://aparshukov.ru"));
            // подключаем CORS
            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseRouting();
            app.UseEndpoints(endpoints =>endpoints.MapControllers());


        }
    }

}