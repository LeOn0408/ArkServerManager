using ARKServerManager.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ARKServerManager.Database
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Server> Server { get; set; }
        public DbSet<PlayerStatistics> Statistics { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "ark.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Server>().Property(x => x.Visible).HasDefaultValue(0);//по умолчанию скрыть
        }
    }
}
