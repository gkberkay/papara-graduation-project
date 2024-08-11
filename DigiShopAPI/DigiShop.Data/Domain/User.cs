using DigiShop.Base.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigiShop.Data.Domain
{
    [Table("User", Schema = "dbo")]

    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public decimal DigitalWallet { get; set; }
        public decimal PointsBalance { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
