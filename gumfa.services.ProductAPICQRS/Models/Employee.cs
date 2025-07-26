using System.ComponentModel.DataAnnotations;

namespace gumfa.services.ProductAPICQRS.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EMPCode { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
