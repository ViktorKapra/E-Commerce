using ECom.Data.Account;

namespace ECom.Data.Models
{
    public class ProductRating
    {

        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public Product Product { get; set; }
        public EComUser User { get; set; }
    }
}
