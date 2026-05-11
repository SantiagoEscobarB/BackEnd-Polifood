using BackendPolifood.Models;
using BackendPolifood.Models.Users;
using BackendPolifood.Models.Products;
using BackendPolifood.Models.Orders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendPolifood.DAO
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasOne(p => p.store)
                .WithMany(s => s.products)
                .HasForeignKey(p => p.storeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
                .HasOne(o => o.store)
                .WithMany(s => s.orders)
                .HasForeignKey(o => o.storeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
                .HasOne(o => o.student)
                .WithMany()
                .HasForeignKey(o => o.studentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderItem>()
                .HasOne(i => i.order)
                .WithMany(o => o.items)
                .HasForeignKey(i => i.orderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderItem>()
                .HasOne(i => i.product)
                .WithMany(p => p.orderItems)
                .HasForeignKey(i => i.productId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}