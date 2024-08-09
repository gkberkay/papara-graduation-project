using DigiShop.Base.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShop.Data.Domain
{
    [Table("Coupon", Schema = "dbo")]

    public class Coupon : BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
