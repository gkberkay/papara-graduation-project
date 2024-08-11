using DigiShop.Base.Schema;

namespace DigiShop.Schema
{
    public class ProductRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
        public decimal PointsPercentage { get; set; }
        public decimal Price { get; set; }
        public decimal MaxPoints { get; set; }
        public bool IsActive { get; set; }

    }

    public class ProductResponse : BaseResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
        public decimal PointsPercentage { get; set; }
        public decimal Price { get; set; }
        public decimal MaxPoints { get; set; }
        public bool IsActive { get; set; }
    }
}
