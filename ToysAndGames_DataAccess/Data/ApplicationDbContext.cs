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


        //TODO: Move this to the upper section, usually the classes structure is Properties, Constructors and the Methods
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: There is a ApplyConfigurationFromAssembly Method

            modelBuilder.ApplyConfiguration(new FluentProductConfig());
        }
    }
}
