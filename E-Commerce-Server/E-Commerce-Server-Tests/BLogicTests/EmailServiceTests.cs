using ECom.BLogic.Services.EmailService;
using ECom.Configuration.Settings;
using FakeItEasy;
using Microsoft.Extensions.Options;
using System.Net.Mail;
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
