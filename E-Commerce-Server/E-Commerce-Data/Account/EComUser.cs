using Microsoft.AspNetCore.Identity;

namespace ECom.Data.Account
{
    public class EComUser : IdentityUser<Guid>
    {
        public string? AddressDelivery { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public EComUser() : base() { }

    }
}
