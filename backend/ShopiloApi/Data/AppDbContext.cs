using Microsoft.EntityFrameworkCore;
using ShopiloApi.Models;

namespace ShopiloApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductTag> ProductTags => Set<ProductTag>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(product => product.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Product>().Property(product => product.DiscountPercentage).HasPrecision(5, 2);
        modelBuilder.Entity<Product>().ToTable(table =>
        {
            table.HasCheckConstraint("CK_Products_Price", "[Price] >= 0");
            table.HasCheckConstraint("CK_Products_Stock", "[Stock] >= 0");
        });

        modelBuilder.Entity<CartItem>()
            .HasIndex(item => new { item.CartId, item.ProductId })
            .IsUnique();

        modelBuilder.Entity<CartItem>()
            .HasOne(item => item.Cart)
            .WithMany(cart => cart.CartItems)
            .HasForeignKey(item => item.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne(item => item.Product)
            .WithMany()
            .HasForeignKey(item => item.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>().Property(order => order.Subtotal).HasPrecision(18, 2);
        modelBuilder.Entity<Order>().Property(order => order.DiscountAmount).HasPrecision(18, 2);
        modelBuilder.Entity<Order>().Property(order => order.ShippingCost).HasPrecision(18, 2);
        modelBuilder.Entity<Order>().Property(order => order.Total).HasPrecision(18, 2);
        modelBuilder.Entity<Order>().HasIndex(order => order.OrderNumber).IsUnique();

        modelBuilder.Entity<OrderItem>().Property(item => item.UnitPrice).HasPrecision(18, 2);
        modelBuilder.Entity<OrderItem>().Property(item => item.LineTotal).HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .HasOne(item => item.Order)
            .WithMany(order => order.Items)
            .HasForeignKey(item => item.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
