using ECom.BLogic.Services.Interfaces;
using ECom.Configuration.Settings;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net.Mail;

namespace ECom.BLogic.Services.EmailService
{
    public class EmailService : IEmailService
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
                Log.Error(ex.Message);
            }
        }
    }
}
