using Microsoft.EntityFrameworkCore;

namespace ChartApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; initial catalog=ChartExampleDb;User Id=gokce;password=Gokce@3535++; Trusted_Connection=true");
        }

        public DbSet<Population> Populations => Set<Population>();
    }
}
