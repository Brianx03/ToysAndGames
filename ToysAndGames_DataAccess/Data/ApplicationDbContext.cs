using Microsoft.EntityFrameworkCore;
using ToysAndGames_DataAccess.Data.FluentConfig;
using ToysAndGames_Model.Models;

namespace ToysAndGames_DataAccess.Data
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
