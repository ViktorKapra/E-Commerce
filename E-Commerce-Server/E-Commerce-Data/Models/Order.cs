namespace ECom.Data.Models
{
    public class Order
    {
        public int OrderListId { get; set; }
        public OrderList OrderList { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
