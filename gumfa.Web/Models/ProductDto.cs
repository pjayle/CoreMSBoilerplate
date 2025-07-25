using System.ComponentModel.DataAnnotations;

namespace gumfa.Web.Models
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal MRP { get; set; }
    }

    public class ProductListDto
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal MRP { get; set; }
    }

    public class ProductAddDto
    {
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal MRP { get; set; }
    }

    public class ProductUpdateDto
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal MRP { get; set; }
    }
}
