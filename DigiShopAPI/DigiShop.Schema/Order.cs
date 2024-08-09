using DigiShop.Base.Schema;

namespace DigiShop.Schema
{
    public class OrderRequest : BaseRequest
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsUsed { get; set; }
        public string OrderNumber { get; set; }
    }

    public class OrderResponse : BaseResponse
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? CouponAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsUsed { get; set; }
        public string OrderNumber { get; set; }
    }
}
