using DigiShop.Base.Entity;

namespace DigiShop.Data.Domain
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public decimal DigitalWallet { get; set; }
        public decimal PointsBalance { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
