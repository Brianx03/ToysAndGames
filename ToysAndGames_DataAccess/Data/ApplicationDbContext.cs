using Microsoft.EntityFrameworkCore;
using ToysAndGamesDataAccess.Data.FluentConfig;
using ToysAndGamesModel.Models;

namespace ToysAndGamesDataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FluentProductConfig());
        }
    }
}
