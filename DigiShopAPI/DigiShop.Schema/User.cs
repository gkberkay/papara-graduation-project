using DigiShop.Base.Schema;
using System.Text.Json.Serialization;

namespace DigiShop.Schema
{
    public class UserRequest : BaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string? Role { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }
        public decimal DigitalWallet { get; set; }
        public decimal PointsBalance { get; set; }
    }

    public class UserResponse : BaseResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public decimal DigitalWallet { get; set; }
        public decimal PointsBalance { get; set; }
    }
}
