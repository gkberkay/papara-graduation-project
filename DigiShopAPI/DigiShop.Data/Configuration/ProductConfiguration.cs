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

            builder.Property(p => p.PointsPercentage)
                .IsRequired();

            builder.Property(p => p.MaxPoints)
                .IsRequired();

            builder.HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ToTable("Product");
        }
    }
}
