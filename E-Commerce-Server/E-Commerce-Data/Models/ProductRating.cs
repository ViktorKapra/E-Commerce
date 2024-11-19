using ECom.Data.Account;
using ECom.Data.Interfaces;

namespace ECom.Data.Models
{
    public class ProductRating : ISoftDeletable
    {

        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public Product Product { get; set; }
        public EComUser User { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
