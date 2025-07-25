using System.ComponentModel.DataAnnotations;

namespace gumfa.services.MasterAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal MRP { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
