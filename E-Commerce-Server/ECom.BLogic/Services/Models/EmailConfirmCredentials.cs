using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLogic.Services.Models
{
    public class EmailConfirmCredentials
    {
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
    }
}
