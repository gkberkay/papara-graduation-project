using DigiShop.Base.Entity;

namespace DigiShop.Data.Domain
{
    public class Coupon : BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
