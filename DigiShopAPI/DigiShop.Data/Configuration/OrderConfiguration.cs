using DigiShop.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiShop.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

            builder.Property(o => o.TotalAmount)
                .IsRequired();

            builder.Property(o => o.CouponAmount)
                .IsRequired();

            builder.Property(o => o.CouponCode)
                .HasMaxLength(10)
                .IsRequired(false);

            builder.Property(o => o.PointsUsed)
                .IsRequired();

            builder.Property(o => o.OrderNumber)
                .HasMaxLength(9)
                .IsRequired();

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Order");
        }
    }
}
