
using ECom.Data.Account;
using ECom.Data.Models;
using ECom.Data.Seeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECom.Data
{
    public class ApplicationDbContext
    : IdentityDbContext<EComUser, EComRole, Guid>
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Seeder seeder = new Seeder(modelBuilder);
            seeder.SeedProducts();

        }
    }
}
