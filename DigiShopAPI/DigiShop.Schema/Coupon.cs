using DigiShop.Base.Schema;

namespace DigiShop.Schema
{
    public class CouponRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiredDate { get; set; }
    }

    public class CouponResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
