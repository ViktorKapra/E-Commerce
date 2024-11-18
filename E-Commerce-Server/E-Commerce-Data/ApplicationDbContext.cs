
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

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Platform);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.DateCreated).IsDescending();
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.TotalRating).IsDescending();
            modelBuilder.Entity<Product>()
                .Property(p => p.TotalRating)
                .HasPrecision(2, 1);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(u => !u.IsDeleted);

            Seeder seeder = new Seeder(modelBuilder);
            seeder.SeedProducts();

        }
    }
}
