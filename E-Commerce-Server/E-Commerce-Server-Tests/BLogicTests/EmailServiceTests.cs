using ECom.Configuration.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using ECom.Data;
using Microsoft.AspNetCore.Identity;
using ECom.BLogic.Services.EmailService;
using Microsoft.Extensions.Options;
using Serilog;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ECom.Test.BLogicTests
{
    public class EmailServiceTests
    {
        private readonly IOptions<SmtpServerSettings> _settings;
        private readonly SmtpClient _client;

        public EmailServiceTests()
        {
            _settings = A.Fake<IOptions<SmtpServerSettings>>();
            _client = A.Fake<SmtpClient>();
        }

        [Fact]
        public async Task SendEmailAsync_ShouldSendEmail()
        {
            // Arrange
            var emailService = new EmailService(_settings, _client);
            var recipient = "test@example.com";
            var subject = "Test Email";
            var body = "This is a test email.";

            // Act
            await emailService.SendEmailAsync(recipient, subject, body);

            // Assert
            //A.CallTo(() => _client.SendMailAsync(A<MailMessage>._)).MustHaveHappenedOnceExactly();
            // To be implemented
        }
    }
}
