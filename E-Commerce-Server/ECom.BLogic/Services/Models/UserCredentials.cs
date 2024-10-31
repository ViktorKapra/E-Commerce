using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLogic.Services.Models
{
    public class UserCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
