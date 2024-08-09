namespace DigiShop.Base.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string InsertUser { get; set; } = "System";
        public DateTime InsertDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}

