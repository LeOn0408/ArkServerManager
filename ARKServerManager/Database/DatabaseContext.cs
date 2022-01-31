using ARKServerManager.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ARKServerManager.Database
{
    public class DatabaseContext : DbContext
    {
        public IConfiguration AppConfiguration { get; private set; }
        public DbSet<Server> Server { get; set; }
        public DbSet<PlayerStatistics> Statistics { get; set; }
        public DbSet<ServerTask> Jobs { get; set; }
        public DbSet<SteamCMD> SteamCMD { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
