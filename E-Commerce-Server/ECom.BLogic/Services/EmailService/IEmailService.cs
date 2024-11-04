using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLogic.Services.EmailService
{
    public interface IEmailService 
    {
        public Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
