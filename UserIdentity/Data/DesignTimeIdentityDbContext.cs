using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserIdentity.Data
{
    public class DesignTimeIdentityDbContext : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Initial Catalog=VLPI_Identity_db;Integrated Security=True");
            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}
