using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.Order
{
    public class OrderExchange
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public uint Quantity { get; set; }
    }
}
