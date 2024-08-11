using DigiShop.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiShop.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(250);

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.PointsPercentage)
                .IsRequired();

            builder.Property(p => p.MaxPoints)
                .IsRequired();

            builder.HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Product");

            builder.HasData(getSeedProductData());
        }


        private Product[] getSeedProductData()
        {
            return new Product[]
            {
                new Product
                {
                    Id = 1,
                    Name = "KnightOnline",
                    Description = "Oyun",
                    StockCount = 5,
                    PointsPercentage = 5,
                    Price = 25,
                    IsActive = true,
                },
                new Product
                {
                    Id = 2,
                    Name = "Csgo",
                    Description = "Oyun",
                    StockCount = 10,
                    PointsPercentage = 3,
                    Price = 15,
                    IsActive = true,
                },
                new Product
                {
                    Id = 3,
                    Name = "LeagueOfLegends",
                    Description = "Oyun",
                    StockCount = 8,
                    PointsPercentage = 4,
                    Price = 20,
                    IsActive = true,
                }
            };
        }
    }
}
