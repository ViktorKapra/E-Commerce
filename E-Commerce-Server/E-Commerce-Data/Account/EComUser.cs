using ECom.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace ECom.Data.Account
{
    public class EComUser : IdentityUser<Guid>
    {
        public string? AddressDelivery { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
        public ICollection<OrderList> OrderLists { get; set; }
        public EComUser() : base() { }

    }
}
