using DigiShop.Base.Schema;

namespace DigiShop.Schema
{
    public class ProductCategoryRequest : BaseRequest
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductCategoryResponse : BaseResponse
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
