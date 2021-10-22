
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
            string TypeDB = "MySQL";
            if (TypeDB == "MySQL")
            {
                IConfigurationSection connStrings = Configuration.GetSection("ConnectionStrings");
                string connection = connStrings.GetSection("MySQLConnection").Value;
                ServerVersion vesrion = ServerVersion.AutoDetect(connection);
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseMySql(connection, vesrion));
                
            }
            else
            {
                IConfigurationSection connStrings = Configuration.GetSection("ConnectionStrings");
                string connectionString = connStrings.GetSection("SQLiteConnection").Value;
                var connection = new SqliteConnection(connectionString);
                services.AddDbContext<DatabaseContext>(options =>
                   options.UseSqlite(connection));
            }

            services.AddHostedService<Jobs>();
        }
    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext db)
        {
            db.Database.EnsureCreated();
            db.Database.Migrate();
            
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>endpoints.MapControllers());


        }
    }

}