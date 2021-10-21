
using Microsoft.Data.Sqlite;
using ARKServerManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.Hosting;

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
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
            
        }
    
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }
            using (var db = new DatabaseContext())
            {
                //db.Database.EnsureCreated();
                db.Database.Migrate();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>endpoints.MapControllers());


        }
    }

}