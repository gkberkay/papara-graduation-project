
using DigiShop.Base.Entity;

namespace DigiShop.Data.Domain
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}