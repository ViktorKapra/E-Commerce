using ECom.Data.Account;

namespace ECom.Data.Models
{
    public class OrderList
    {
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CustomerId { get; set; }
        public EComUser Customer { get; set; }
        public bool IsFinalized { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
