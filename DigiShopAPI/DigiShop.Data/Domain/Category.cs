
using DigiShop.Base.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShop.Data.Domain
{
    [Table("Category", Schema ="dbo")]
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Tags { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}