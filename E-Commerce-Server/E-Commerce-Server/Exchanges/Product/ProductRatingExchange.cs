using System.ComponentModel.DataAnnotations;

namespace ECom.API.Exchanges.Product
{
    public class ProductRatingExchange
    {
        public int ProductId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
