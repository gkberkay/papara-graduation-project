using DigiShop.Base.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShop.Data.Domain
{
    [Table("Product", Schema = "dbo")]

    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
        public decimal PointsPercentage { get; set; }
        public int Price { get; set; }
        public decimal MaxPoints { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
