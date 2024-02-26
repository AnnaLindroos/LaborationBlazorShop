using LaborationBlazorShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LaborationBlazorShop.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> ProductsInCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>()
                .HasOne<ApplicationUser>(c => c.ApplicationUser)
                .WithOne(u => u.Cart)
                .HasForeignKey<ApplicationUser>(u => u.CartId);

            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => cp.Id);
        }
    }
}
