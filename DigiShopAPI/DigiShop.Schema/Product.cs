using DigiShop.Base.Schema;

namespace DigiShop.Schema
{
    public class ProductRequest : BaseRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal PointsPercentage { get; set; }
        public decimal MaxPoints { get; set; }
    }

    public class ProductResponse : BaseResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal PointsPercentage { get; set; }
        public decimal MaxPoints { get; set; }
    }
}
