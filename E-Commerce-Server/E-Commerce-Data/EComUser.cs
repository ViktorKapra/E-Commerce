using Microsoft.AspNetCore.Identity;

namespace ECom.Data
{
    public class EComUser:IdentityUser<Guid>
    {
        public EComUser():base(){ }

    }
}
