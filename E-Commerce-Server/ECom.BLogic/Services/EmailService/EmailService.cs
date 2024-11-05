using ECom.Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using MimeKit;
using Serilog;
using Serilog.Context;
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
        private SmtpClient _client;
        public EmailService(IOptions<SmtpServerSettings> settings, SmtpClient client)
        {
            _settings = settings.Value;
            _client = client;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        { 

            MailMessage msg = new MailMessage();
            
                try
                {
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.From = new MailAddress("Ecom@mail.com");
                    msg.To.Add(toEmail);
                    msg.IsBodyHtml = true;
                    _client.Host = _settings.Host;
                    System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential(_settings.Email, _settings.Password);
                    _client.Port = int.Parse(_settings.Port);
                    _client.EnableSsl = true;
                    _client.UseDefaultCredentials = false;
                    _client.Credentials = basicauthenticationinfo;
                    _client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await _client.SendMailAsync(msg);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    Log.Error(ex.Message);
                }           
        }
    }
}
