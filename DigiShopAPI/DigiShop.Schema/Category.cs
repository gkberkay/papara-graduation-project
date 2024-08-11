using DigiShop.Base.Schema;

namespace DigiShop.Schema
{
    public class CategoryRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }
    }

    public class CategoryResponse : BaseResponse
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }
    }
}
