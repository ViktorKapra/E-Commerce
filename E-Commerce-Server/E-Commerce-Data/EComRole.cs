using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Data
{
    public class EComRole: IdentityRole<Guid>
    {
        public EComRole(string name):base(name) { }
    }
}
