using DigiShop.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiShop.Data.Configuration
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(ct => ct.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

            builder.Property(ct => ct.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(ct => ct.SalePrice)
                .IsRequired();

            builder.Property(ct => ct.IsActive)
                .IsRequired();

            builder.Property(ct => ct.ExpiredDate)
                .IsRequired();

            builder.ToTable("Coupon");
        }
    }
}
