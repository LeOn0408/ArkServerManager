using ArkWeb.Models.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArkWeb
{
    public class ApplicationContext : IdentityDbContext<ArkUsers>
    {
        public DbSet<ArkNews> View_ArkNews { get; set; }
        public DbSet<ArkOAuth> OAuth { get; set; }
        public DbSet<ArkPlayers> Ark_players { get; set; }
        public DbSet<ArkJobs> Ark_Jobs { get; set; }
        

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
    
}
