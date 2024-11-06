using Microsoft.AspNetCore.Identity;

namespace ECom.Data.Account
{
    public class EComRole : IdentityRole<Guid>
    {
        public EComRole(string name) : base(name) { }
    }
}
