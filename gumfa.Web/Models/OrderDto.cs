namespace gumfa.Web.Models.DTO
{
    public class OrderListDto
    {
        public int OrderID { get; set; }
        public string? OrderName { get; set; }
        public string? Status { get; set; }
    }

    public class OrderAddDto
    {
        public string? OrderName { get; set; }
        public string? Status { get; set; }
    }

    public class OrderUpdateDto
    {
        public int OrderID { get; set; }
        public string? OrderName { get; set; }
        public string? Status { get; set; }
    }
}
