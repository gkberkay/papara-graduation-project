using DigiShop.Base.Entity;

namespace DigiShop.Data.Domain
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsUsed { get; set; }
        public string OrderNumber { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
