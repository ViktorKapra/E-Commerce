using ECom.Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using MimeKit;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLogic.Services.EmailService
{
    public class EmailService: IEmailService
    {
        private readonly SmtpServerSettings _settings;
        public EmailService(IOptions<SmtpServerSettings> settings) 
        {
            _settings = settings.Value;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        { 

            MailMessage msg = new MailMessage();
            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient())
            {
                try
                {
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.From = new MailAddress("Ecom@mail.com");
                    msg.To.Add(toEmail);
                    msg.IsBodyHtml = true;
                    client.Host = _settings.Host;
                    System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential(_settings.Email, _settings.Password);
                    client.Port = int.Parse(_settings.Port);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicauthenticationinfo;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Log.Error(ex.Message);
                }
            }
        }
    }
}
