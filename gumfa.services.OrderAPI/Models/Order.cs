using System.ComponentModel.DataAnnotations;

namespace gumfa.services.OrderAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int OrderBy { get; set; }
        public DateTime OrderOn { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
