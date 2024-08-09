

using DigiShop.Base.Entity;

namespace DigiShop.Data.Domain 
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
        public decimal PointsPercentage { get; set; }
        public decimal MaxPoints { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
