using educational_practice5.Models;
using educational_practice5.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace educational_practice5.Database
{
    public class RepositoryContext:IdentityDbContext<ApplicationUser>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> products { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<Order> orders { get; set; }
        public override int SaveChanges()
        {
            
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}