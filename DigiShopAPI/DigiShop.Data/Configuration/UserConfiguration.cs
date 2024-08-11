using DigiShop.Base.Helpers;
using DigiShop.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiShop.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Status)
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();

            builder.Property(u => u.DigitalWallet)
                .IsRequired();

            builder.Property(u => u.PointsBalance)
                .IsRequired();

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("User");

            builder.HasData(getFirstAdminData());
        }

        private User getFirstAdminData()
        {
            return new User
            {
                Id = 1,
                FirstName = "Gökberk",
                LastName = "Ay",
                UserName = "gokberk",
                Email = "gokberk.ay@gmail.com",
                Password = HashHelper.CreateMD5("12345678"),
                Role = "Admin",
                Status = true
            };
        }
    }
}
