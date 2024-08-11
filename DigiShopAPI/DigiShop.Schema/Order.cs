using DigiShop.Base.Schema;
using System.Text.Json.Serialization;

namespace DigiShop.Schema
{
    public class OrderRequest : BaseRequest
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public List<OrderProduct> Products { get; set; }
        public string? CouponCode { get; set; }
        public CreditCardInfo CreditCardInfo { get; set; }
    }

    public class OrderProduct
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderProductResponse
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreditCardInfo
    {
        public string CardNo { get; set; }
        public int CVV { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
    }

    public class OrderResponse : BaseResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? CouponAmount { get; set; }
        public string? CouponCode { get; set; }
        public decimal PointsUsed { get; set; }
        public string OrderNumber { get; set; }
        public bool IsActive { get; set; }
        public List<OrderProductResponse> OrderDetails { get; set; }
    }
}
