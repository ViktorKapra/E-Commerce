using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.Order
{
    public class OrderExchange
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be positve number")]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be positve number")]
        public int Quantity { get; set; }
    }
}
