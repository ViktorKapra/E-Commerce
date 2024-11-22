
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

        public DbSet<ProductRating> ProductRatings { get; set; }

        public DbSet<OrderList> OrderLists { get; set; }

        public DbSet<Order> Orders { get; set; }

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
                .HasPrecision(2, 1)
                .HasDefaultValue(0);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Genre);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Product>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Ratings)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId);
            modelBuilder.Entity<Product>()
                .HasMany(e => e.AssociatedOrders)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId);


            modelBuilder.Entity<ProductRating>()
                .Property(x => x.Rating)
                .IsRequired();
            modelBuilder.Entity<ProductRating>()
                .HasKey(e => new { e.ProductId, e.UserId });

            modelBuilder.Entity<OrderList>()
                .Property(x => x.IsFinalized)
                .HasDefaultValue(false);
            modelBuilder.Entity<OrderList>()
                .Property(x => x.CustomerId)
                .IsRequired();
            modelBuilder.Entity<OrderList>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<OrderList>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.OrderList)
                .HasForeignKey(e => e.OrderListId);

            modelBuilder.Entity<Order>()
                .HasKey(e => new { e.OrderListId, e.ProductId });

            modelBuilder.Entity<EComUser>()
                .HasKey(user => user.Id);
            modelBuilder.Entity<EComUser>()
                .HasMany(e => e.Ratings)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);


            Seeder seeder = new Seeder(modelBuilder);
            seeder.SeedProducts();

        }
    }
}
