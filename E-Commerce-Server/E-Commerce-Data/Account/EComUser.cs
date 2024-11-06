using Microsoft.AspNetCore.Identity;

namespace ECom.Data.Account
{
    public class EComUser : IdentityUser<Guid>
    {
        public EComUser() : base() { }

    }
}
