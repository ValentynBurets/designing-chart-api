using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityUser.Data
{
    public class IdentityUserContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityUserContext(DbContextOptions<IdentityUserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
