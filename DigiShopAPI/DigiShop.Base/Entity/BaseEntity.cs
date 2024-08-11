using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigiShop.Base.Entity
{
    public class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string InsertUser { get; set; } = "System";
        public DateTime InsertDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}

