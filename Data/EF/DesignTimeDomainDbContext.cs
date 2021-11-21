using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.EF
{
    public class DesignTimeDomainDbContext : IDesignTimeDbContextFactory<DomainDbContext>
    {
        public DomainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DomainDbContext>();
            // pass your design time connection string here
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Initial Catalog=VLPI_db;Integrated Security=True");
            return new DomainDbContext(optionsBuilder.Options);
        }
    }
}
