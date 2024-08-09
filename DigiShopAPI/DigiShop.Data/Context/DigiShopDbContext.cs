using DigiShop.Data.Configuration;
using DigiShop.Data.Domain;
using Microsoft.EntityFrameworkCore;

public class DigiShopDbContext : DbContext
{
    public DigiShopDbContext(DbContextOptions<DigiShopDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
        modelBuilder.ApplyConfiguration(new CouponConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
    }


}
